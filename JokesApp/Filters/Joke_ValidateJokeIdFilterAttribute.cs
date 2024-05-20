using JokesApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JokesApp.Filters
{
    public class Joke_ValidateJokeIdFilterAttribute : ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var jokeId = filterContext.ActionArguments["id"] as int?;

            if(jokeId.HasValue)
            {
                if(jokeId.Value <= 0)
                {
                    filterContext.ModelState.AddModelError("ID","Joke ID is invalid");
                    var problemDetails = new ValidationProblemDetails(filterContext.ModelState);
                    filterContext.Result = new BadRequestObjectResult(problemDetails);

                }
            }
        }
    }
}
