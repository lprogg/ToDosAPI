using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project
{
    public class StatusMethods : ControllerBase
    {
        public ActionResult PostStatus()
        {
            return StatusCode(StatusCodes.Status404NotFound,
                new
                {
                    Message="There was an error while trying to save the record."
                });
        }

        public ActionResult Status()
        {
            return StatusCode(StatusCodes.Status404NotFound,
                new
                {
                    Message="No records were found."
                });
        }
    }
}