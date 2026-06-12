using Microsoft.AspNetCore.Mvc;
using Model_Layer;
using Data_Logic_Layer;
using Service_Logic_Layer;

namespace CardCartAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly CartService _cartService;

        public CartsController(IDataService dataService, CartService cartService)
        {
            _dataService = dataService;
            _cartService = cartService;
        }

        // GET /Carts — Get all cart items
        [HttpGet(Name = "GetAllCarts")]
        public ActionResult<List<Models.Carts>> GetAll()
        {
            return Ok(_dataService.cartlist);
        }

        // GET /Carts/total — Get grand total of all cart items
        [HttpGet("total", Name = "GetCartTotal")]
        public ActionResult<string> GetTotal()
        {
            string result = _cartService.CheckTotal(_dataService.cartlist);
            return Ok(result);
        }

        // GET /Carts/discount — Get discount info based on grand total
        [HttpGet("discount", Name = "GetCartDiscount")]
        public ActionResult<string> GetDiscount()
        {
            string result = _cartService.CheckDiscount(_dataService.cartlist);
            return Ok(result);
        }

        // POST /Carts — Add a new cart item
        [HttpPost(Name = "AddCart")]
        public ActionResult Add([FromBody] Models.Carts newCart)
        {
            if (newCart == null)
                return BadRequest("Cart item cannot be null.");

            bool result = _dataService.AddCart(newCart.Name, newCart.Price, newCart.Quantity);

            if (!result)
                return StatusCode(500, "Failed to add cart item.");

            return Ok("Cart item added successfully.");
        }

        // PUT /Carts/{choice} — Update a cart item by list position (1-based)
        [HttpPut("{choice}", Name = "UpdateCart")]
        public ActionResult Update(int choice, [FromBody] Models.Carts updatedCart)
        {
            if (updatedCart == null)
                return BadRequest("Cart data cannot be null.");

            bool result = _dataService.UpdateCart(choice, updatedCart.Name, updatedCart.Price, updatedCart.Quantity);

            if (!result)
                return NotFound($"No cart item found at position {choice}.");

            return Ok("Cart item updated successfully.");
        }

        // DELETE /Carts/{choice} — Delete a cart item by list position (1-based)
        [HttpDelete("{choice}", Name = "DeleteCart")]
        public ActionResult Delete(int choice)
        {
            bool result = _dataService.DeleteCart(choice);

            if (!result)
                return NotFound($"No cart item found at position {choice}.");

            return Ok("Cart item deleted successfully.");
        }
    }
}