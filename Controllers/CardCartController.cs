using Microsoft.AspNetCore.Mvc;
using Model_Layer;
using Data_Logic_Layer;

namespace CardCartAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public CardsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // Cards
        [HttpGet(Name = "GetAllCards")]
        public ActionResult<List<Models.Cards>> GetAll()
        {
            return Ok(_dataService.cardlist);
        }

        // Add 
        [HttpPost(Name = "AddCard")]
        public ActionResult Add([FromBody] Models.Cards newCard)
        {
            if (newCard == null)
                return BadRequest("Card cannot be null.");

            bool result = _dataService.AddCard(newCard.ID, newCard.Name);

            if (!result)
                return StatusCode(500, "Failed to add card.");

            return Ok("Card added successfully.");
        }

        // Update 
        [HttpPut("{choice}", Name = "UpdateCard")]
        public ActionResult Update(int choice, [FromBody] Models.Cards updatedCard)
        {
            if (updatedCard == null)
                return BadRequest("Card data cannot be null.");

            bool result = _dataService.UpdateCard(choice, updatedCard.Name, updatedCard.ID);

            if (!result)
                return NotFound($"No card found at position {choice}.");

            return Ok("Card updated successfully.");
        }

        // Delete 
        [HttpDelete("{choice}", Name = "DeleteCard")]
        public ActionResult Delete(int choice)
        {
            bool result = _dataService.DeleteCard(choice);

            if (!result)
                return NotFound($"No card found at position {choice}.");

            return Ok("Card deleted successfully.");
        }
    }
}