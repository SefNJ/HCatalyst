using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HCatalyst.Models;

namespace HCatalyst.Controllers
{
    [Route("api/HCpersons")]
    [ApiController]
    public class HCpersonsController : ControllerBase
    {
        private readonly HCcontext m_context;

        public HCpersonsController(HCcontext context)
        {
            m_context = context;
        }

        /// <summary>
        /// Returns all the entries in the EF that have been created up to this point.
        /// </summary>
        /// <returns>A JSON list of entries</returns>
        // GET: api/HCpersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HCperson>>> GetHCPersons()
        {
            return await m_context.HCpersons.ToListAsync();
        }

        /// <summary>
        /// Returns one entry that matches the given iD.
        /// </summary>
        /// <param name="id">The uniquie identifier for the entry that had been created.</param>
        /// <returns>A JSON format for the found entry.  If not found status code 404 is returned.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<HCperson>> GetHCperson(long id)
        {
            var hcPerson = await m_context.HCpersons.FindAsync(id);

            if (hcPerson == null)
            {
                return NotFound();
            }

            return hcPerson;
        }

        /// <summary>
        /// Returns the given entry when either the FirstName or LastName is specified.  If both values are specified, then the result has to match both values.  The comparison is case sesntive and en exach match.
        /// </summary>
        /// <param name="i_oHCperson">Either the FirstName or Lastname or Both muse be given for the Search to work correctly.</param>
        /// <param name="i_oHCparam">The Delay paramater is the number of seconds the response will be delayed.  Its range is from 0 to 255.</param>
        /// <returns>The array of entries that match the given criteria are returned.</returns>
        [HttpPost("Search/")]
        public async Task<ActionResult<List<HCperson>>> SearchHCperson(HCperson i_oHCperson, [FromQuery] HCparameter i_oHCparam)
        {
            if (i_oHCparam.Delay > 0) {
                await Task.Delay(TimeSpan.FromSeconds(i_oHCparam.Delay));
            }

            if (i_oHCperson == null)
            {
                return NotFound();
            }

            if (String.IsNullOrEmpty(i_oHCperson.FirstName) && String.IsNullOrEmpty(i_oHCperson.LastName))
            {
                ValidationProblemDetails oValDetails = new ValidationProblemDetails();
                oValDetails.Title = "At least First Name or Last Name must be supplied.";
                return ValidationProblem(oValDetails);
            }

            ActionResult<List<HCperson>> hcPerson = await GetHCpersonToSearch(i_oHCperson);

            if (hcPerson == null)
            {
                return NotFound();
            }

            return hcPerson;
        }
        
        /// <summary>
        /// Uses the whera clause to find the entry that matches exactly to the given name.  It will mtach either the FirstName, LastName or both in the search.  It uses an exact comparison for the values.
        /// </summary>
        /// <param name="i_oHCperson">Only the FirstName or LastName are requied to be populated.</param>
        /// <returns></returns>
        private async Task<ActionResult<List<HCperson>>> GetHCpersonToSearch(HCperson i_oHCperson)
        {
            List<HCperson> hcPerson;
            if (String.IsNullOrEmpty(i_oHCperson.FirstName))
            {
                hcPerson = await(m_context.HCpersons.Where(sb1 => sb1.LastName == i_oHCperson.LastName).ToListAsync<HCperson>());
            }
            else if (String.IsNullOrEmpty(i_oHCperson.LastName))
            {
                hcPerson = await (m_context.HCpersons.Where(sb2 => sb2.FirstName == i_oHCperson.FirstName).ToListAsync<HCperson>());
            }
            else
            {
                hcPerson = await (m_context.HCpersons.Where(sb3 => sb3.LastName == i_oHCperson.LastName).Where(s4 => s4.FirstName == i_oHCperson.FirstName).ToListAsync<HCperson>());
            }
            return hcPerson;
        }

        /// <summary>
        /// Returns the given entry when either the FirstName or LastName is specified.  If both values are specified, then the result has to match both values.  The comparison is case insesntive and the string contains is used for comparison..
        /// </summary>
        /// <param name="i_oHCperson">Only the FirstName or LastName are requied to be populated.</param>
        /// <param name="i_oHCparam">The Delay paramater is the number of seconds the response will be delayed.  Its range is from 0 to 255.</param>
        /// <returns></returns>
        [HttpPost("SearchContains/")]
        public async Task<ActionResult<List<HCperson>>> SearchContainsHCperson(HCperson i_oHCperson, [FromQuery] HCparameter i_oHCparam)
        {
            if (i_oHCparam.Delay > 0)
            {
                await Task.Delay(TimeSpan.FromSeconds(i_oHCparam.Delay));
            }

            if (i_oHCperson == null)
            {
                return NotFound();
            }

            if (String.IsNullOrEmpty(i_oHCperson.FirstName) && String.IsNullOrEmpty(i_oHCperson.LastName))
            {
                ValidationProblemDetails oValDetails = new ValidationProblemDetails();
                oValDetails.Title = "At least First Name or Last Name must be supplied.";
                return ValidationProblem(oValDetails);
            }

            ActionResult<List<HCperson>> hcPerson = await GetHCpersonToSearchContains(i_oHCperson);

            if (hcPerson == null)
            {
                return NotFound();
            }

            return hcPerson;
        }

        /// <summary>
        /// Uses the whera clause to find the entry that contains to the given name value.  It will contain either the FirstName, LastName or both in the search.  It uses a contain comparison for the values.
        /// </summary>
        /// <param name="i_oHCperson">Only the FirstName or LastName are requied to be populated.</param>
        /// <returns></returns>
        private async Task<ActionResult<List<HCperson>>> GetHCpersonToSearchContains(HCperson i_oHCperson)
        {
            List<HCperson> hcPerson;
            if (String.IsNullOrEmpty(i_oHCperson.FirstName))
            {
                hcPerson = await (m_context.HCpersons.Where(sb1 => sb1.LastName.Contains(i_oHCperson.LastName, StringComparison.OrdinalIgnoreCase)).ToListAsync<HCperson>());
            }
            else if (String.IsNullOrEmpty(i_oHCperson.LastName))
            {
                hcPerson = await (m_context.HCpersons.Where(sb2 => sb2.FirstName.Contains(i_oHCperson.FirstName, StringComparison.OrdinalIgnoreCase)).ToListAsync<HCperson>());
            }
            else
            {
                hcPerson = await (m_context.HCpersons.Where(sb3 => sb3.LastName.Contains(i_oHCperson.LastName, StringComparison.OrdinalIgnoreCase)).Where(s4 => s4.FirstName.Contains(i_oHCperson.FirstName, StringComparison.OrdinalIgnoreCase)).ToListAsync<HCperson>());
            }
            return hcPerson;
        }


        /// <summary>
        /// Adds the given values into the list of persons.  The FirstName and LastName are both required entries.
        /// </summary>
        /// <param name="i_oHCperson">The JSON format of information that will ve saved</param>
        /// <param name="i_oHCparam">The Delay paramater is the number of seconds the response will be delayed.  Its range is from 0 to 255.</param>
        /// <returns>The JSON format of the entry that was made including the new ID value for th eentry.  If the FirstName or LastName is missing</returns>
        [HttpPost]
        public async Task<ActionResult<HCperson>> CreateHCperson(HCperson i_oHCperson, [FromQuery] HCparameter i_oHCparam)
        {
            if (i_oHCparam.Delay > 0)
            {
                await Task.Delay(TimeSpan.FromSeconds(i_oHCparam.Delay));
            }

            if (String.IsNullOrEmpty(i_oHCperson.FirstName) || String.IsNullOrEmpty(i_oHCperson.LastName))
            {
                ValidationProblemDetails oValDetails = new ValidationProblemDetails();
                oValDetails.Title = "First Name and Last Name values are both required.";
                return ValidationProblem(oValDetails);
            }
            m_context.HCpersons.Add(i_oHCperson);
            await m_context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetHCperson),
                new { id = i_oHCperson.ID},
                i_oHCperson);
        }

        /// <summary>
        /// Wrapperd function to find the entry with the given ID value.
        /// </summary>
        /// <param name="id">The primary ID key value to be searched</param>
        /// <returns></returns>       
        private bool HCpersonExists(long id) =>
             m_context.HCpersons.Any(e => e.ID == id);
    }
}
