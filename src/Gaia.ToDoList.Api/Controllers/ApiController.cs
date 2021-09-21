using System.Linq;
using Gaia.ToDoList.Business.Interfaces.Notifier;
using Gaia.ToDoList.Business.Notifier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gaia.ToDoList.Api.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly INotifier Notifier;

        protected ApiController(INotifier notifier)
        {
            Notifier = notifier;
        }

        protected bool IsValidOperation()
        {
            return !Notifier.HasNotification();
        }

        protected new ActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequestResponse();
        }

        protected new ActionResult BadRequest(ModelStateDictionary modelState)
        {
            NotifyErrorsInvalidInput(modelState);
            return BadRequestResponse();
        }

        protected void NotifyErrorsInvalidInput(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string message)
        {
            Notifier.Handle(new Notification(message));
        }

        private ActionResult BadRequestResponse()
        {
            return BadRequest(new
            {
                success = false,
                errors = Notifier.GetNotifications().Select(n => n.Message)
            });
        }
    }
}
