using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public UsersController(ApplicationDbContext dbC )
        {
             dbContext = dbC;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<user>> Register(user user)
        {
            if (await dbContext.Users.AnyAsync(u => u.Email == user.Email))
            {
                ModelState.AddModelError("email", "Email is already in use");
                return BadRequest(ModelState);
            }
            else if (user != null)
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return Ok(user);
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<user>> Login(string email, string password)
        {
          
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                return Ok(user);
            }
            ModelState.AddModelError("Credentials", "Invalid Email/Password");
            return BadRequest(ModelState);
        }


        [HttpGet("GetProducts")]

        public async Task<ActionResult<Product>> GetAllProducts()
        {
            var products = await dbContext.Products.ToListAsync();

            return Ok(products);
            
            ModelState.AddModelError("Get", "Cannot fetch the data");
            return BadRequest(ModelState);

        }


        [HttpGet("GetProductById/{productId}")]
        public async Task<ActionResult<Product>> GetProductById(int productId)
        {
            if (productId <= 0)
            {
                ModelState.AddModelError("ProductId", "Invalid product ID.");
                return BadRequest(ModelState);
            }

            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {productId} not found." });
            }

            return Ok(product);
        }

    }
}
