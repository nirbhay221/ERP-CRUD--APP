using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core;
using Products.DB;
using System;
using System.Collections.Generic;

namespace Products.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsServices _eventsServices;

        public EventsController(IEventsServices eventsServices)
        {
            _eventsServices = eventsServices;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            try
            {
                var events = _eventsServices.GetEvents();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult GetEvent(int id)
        {
            try
            {
                var eventObj = _eventsServices.GetEvent(id);
                if (eventObj == null)
                {
                    return NotFound("Event not found.");
                }

                return Ok(eventObj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event eventObj)
        {
            if (eventObj == null)
            {
                return BadRequest("Invalid event data.");
            }

            try
            {
                var newEvent = _eventsServices.CreateEvent(eventObj);
                return CreatedAtRoute("GetEvent", new { id = newEvent.Id }, newEvent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult EditEvent([FromBody] Event eventObj)
        {
            if (eventObj == null)
            {
                return BadRequest("Invalid event data.");
            }

            try
            {
                var updatedEvent = _eventsServices.EditEvent(eventObj);
                return Ok(updatedEvent);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteEvent([FromBody] Event eventObj)
        {
            if (eventObj == null)
            {
                return BadRequest("Invalid event data.");
            }

            try
            {
                _eventsServices.DeleteEvent(eventObj);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
