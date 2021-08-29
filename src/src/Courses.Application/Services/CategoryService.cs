using AutoMapper;
using Courses.Application.Services.Interfaces;
using Courses.Application.ViewModels;
using Courses.Domain.Interfaces.Repositories;
using Courses.Domain.Interfaces.UoW;
using Courses.Domain.Models.Category;
using Courses.Domain.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAllAsync());

            return categories;
        }

        public async Task<CategoryViewModel> GetByIdAsync(int id)
        {
            var category = _mapper.Map<CategoryViewModel>(await _categoryRepository.GetByIdAsync(id));

            return category;
        }
    }
}
