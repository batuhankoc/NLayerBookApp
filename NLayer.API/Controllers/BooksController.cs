using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Book> _service;

        public BooksController(IMapper mapper, IService<Book> service)
        {
            _mapper = mapper;
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await _service.GetAllAsync();
            var booksDto = _mapper.Map<List<BookDto>>(books.ToList());
            return Ok(CustomResponseDto<List<BookDto>>.Success(200, booksDto));
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            var booksDto = _mapper.Map<BookDto>(book);
            return Ok(booksDto);
        }
        [HttpPost]
        public async Task<IActionResult> Save(BookDto bookDto)
        {
            var book = await _service.AddAsync(_mapper.Map<Book>(bookDto));
            var booksDto = _mapper.Map<BookDto>(book);
            return Ok(CustomResponseDto<BookDto>.Success(201, booksDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetBookWithPostMethod(GetBookWithPostDto bookDto)
        {
            var book = await _service.GetByIdAsync(bookDto.Id);
            var booksDto = _mapper.Map<BookDto>(book);
            return Ok(booksDto);
        }
        [HttpPut]
        public async Task<IActionResult> Update(BookDto bookDto)
        {
            await _service.UpdateAsync(_mapper.Map<Book>(bookDto));
            return Ok(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<BookDto>(product);
            await _service.RemoveAsync(product);
            return Ok(CustomResponseDto<BookDto>.Success(201, productsDto));
        }
    }
}
