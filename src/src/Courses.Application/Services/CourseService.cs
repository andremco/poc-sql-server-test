using AutoMapper;
using Courses.Application.Services.Interfaces;
using Courses.Application.ViewModels;
using Courses.Domain.Interfaces.Notifications;
using Courses.Domain.Interfaces.Repositories;
using Courses.Domain.Interfaces.UoW;
using Courses.Domain.Models.Course;
using Courses.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IDomainNotification _domainNotification;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IMapper mapper, IDomainNotification domainNotification, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _domainNotification = domainNotification;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _courseRepository = courseRepository;
        }

        public async Task<CourseViewModel> AddAsync(CourseViewModel courseViewModel)
        {
            var model = _mapper.Map<Course>(courseViewModel);
            model.Id = 0;
            model.Category = null;

            var validation = new RegisterCourseValidation().Validate(model);

            if (!validation.IsValid)
            {
                _domainNotification.AddNotifications(validation);
                return courseViewModel;
            }

            await ValidateBusinessRulesAsync(courseViewModel);

            if (!_domainNotification.HasNotifications)
            {
                _courseRepository.Add(model);
                _unitOfWork.Commit();

                var viewModel = _mapper.Map<CourseViewModel>(model);

                return viewModel;
            }

            return courseViewModel;
        }

        public async Task<CourseViewModel> UpdateAsync(CourseViewModel courseViewModel)
        {
            var model = _mapper.Map<Course>(courseViewModel);
            model.Category = null;

            var validation = new UpdateCourseValidation().Validate(model);

            if (!validation.IsValid)
            {
                _domainNotification.AddNotifications(validation);
                return courseViewModel;
            }

            if (await _categoryRepository.GetByIdAsync(courseViewModel.CategoryId) != null)
            {
                await ValidateBusinessRulesAsync(courseViewModel);

                if (!_domainNotification.HasNotifications)
                {
                    _courseRepository.Update(model);
                    _unitOfWork.Commit();

                    var viewModel = _mapper.Map<CourseViewModel>(model);

                    return viewModel;
                }
            }
            else
            {
                _domainNotification.AddNotification("CourseNotFound", "O Curso nao foi encontrado!");
            }

            return courseViewModel;
        }

        private async Task ValidateBusinessRulesAsync(CourseViewModel courseViewModel)
        {

            if (courseViewModel.StartDate < DateTime.Now)
            {
                _domainNotification.AddNotification("StartDateIsLessThanNow", "A data de inicio do curso e menor que hoje!");
            }

            if (courseViewModel.StartDate > courseViewModel.EndDate)
            {
                _domainNotification.AddNotification("InvalidRangeDate", "Range de datas invalidas!");
            }

            var category = await _categoryRepository.GetByIdAsync(courseViewModel.CategoryId);

            if (category == null)
            {
                _domainNotification.AddNotification("CategoryNotFound", "Categoria nao encontrado!");
            }

            if (await _courseRepository.VerifyIfExistsCourseByPeriodDate(courseViewModel.StartDate, courseViewModel.EndDate))
            {
                _domainNotification.AddNotification("PeriodExists", "Existe(m) curso(s) planejados(s) dentro do periodo informado.");
            }
        }

        public async Task<CourseViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<CourseViewModel>(await _courseRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CourseViewModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CourseViewModel>>(await _courseRepository.GetAllAsync());
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _courseRepository.GetByIdAsync(id);

            if (model == null)
            {
                _domainNotification.AddNotification("CourseNotFound", "O Curso nao foi encontrado!");
                return;
            }

            _courseRepository.Remove(model);
            _unitOfWork.Commit();
        }
    }
}
