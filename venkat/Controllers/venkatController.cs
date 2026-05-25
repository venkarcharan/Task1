using Microsoft.AspNetCore.Mvc;
using venkat.Common.Models;

namespace venkat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class venkatController : ControllerBase
    {
        private static List<Details> data = new List<Details>
        {
            new Details{Id=1,Name="utkarsh",Role="Doctor"},
            new Details{Id=2,Name="venkat",Role="Engineer"}


        };

        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var result = data.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }



        [HttpPost]
        public IActionResult Add(Details d)
        {
            data.Add(d);

            return Ok("Added Successfully");
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, Details newData)
        {
            var item = data.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            item.Name = newData.Name;
            item.Role = newData.Role;

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = data.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            data.Remove(item);

            return Ok("Deleted Successfully");
        }
    }
}