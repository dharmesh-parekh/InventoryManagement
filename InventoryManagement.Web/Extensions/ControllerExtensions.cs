using System.Collections.Generic;
using System.Web.Mvc;
using InventoryManagement.Web.Extensions.ExtensionModel;

namespace InventoryManagement.Web.Extensions
{
    /// <summary>
    /// Class ControllerExtensions.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// The message key
        /// </summary>
        private const string MessageKey = "messages";
        /// <summary>
        /// The default error message
        /// </summary>
        private const string DefaultErrorMessage = "An error was encountered.";

        /// <summary>
        /// Adds the validation message.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public static void AddValidationMessage(this Controller controller)
        {
            var validationMessage = controller.ModelState.GetValidationSummary();
            if (!string.IsNullOrEmpty(validationMessage))
                controller.AddErrorMessage(validationMessage);
        }

        /// <summary>
        /// Adds the error message.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public static void AddErrorMessage(this Controller controller)
        {
            controller.AddErrorMessage(DefaultErrorMessage);
        }

        /// <summary>
        /// Adds the error message.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="text">The text.</param>
        public static void AddErrorMessage(this Controller controller, string text)
        {
            var message = new Message
            {
                Text = text,
                Type = Message.MessageType.ERROR
            };
            controller.AddMessage(message);
        }

        /// <summary>
        /// Adds the success message.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="text">The text.</param>
        public static void AddSuccessMessage(this Controller controller, string text)
        {
            var message = new Message
            {
                Text = text,
                Type = Message.MessageType.SUCCESS
            };
            controller.AddMessage(message);
        }

        /// <summary>
        /// Adds the notice message.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="text">The text.</param>
        public static void AddNoticeMessage(this Controller controller, string text)
        {
            var message = new Message
            {
                Text = text,
                Type = Message.MessageType.NOTICE
            };
            controller.AddMessage(message);
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>IList{Message}.</returns>
        public static IList<Message> GetMessages(this Controller controller)
        {
            var messageData = controller.TempData[MessageKey];
            if (messageData != null)
            {
                return (IList<Message>)messageData;
            }
            return new List<Message>();
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        private static void AddMessage(this Controller controller, Message message)
        {
            IList<Message> messages;

            if (controller.TempData[MessageKey] != null)
            {
                messages = (IList<Message>)controller.TempData[MessageKey];
            }
            else
            {
                messages = new List<Message>();
            }

            messages.Add(message);
            controller.TempData[MessageKey] = messages;
        }

        /// <summary>
        /// Adds the messages.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="messages">The messages.</param>
        private static void AddMessages(this Controller controller, List<Message> messages)
        {
            controller.TempData[MessageKey] = messages;
        }

        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>JsonResult.</returns>
        public static JsonResult SuccessResult(this Controller controller)
        {
            return new JsonResult
            {
                Data = true
            };
        }

        /// <summary>
        /// Fails the result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>JsonResult.</returns>
        public static JsonResult FailResult(this Controller controller)
        {
            return new JsonResult
            {
                Data = false
            };
        }

        /// <summary>
        /// Successes the save result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>JsonResult.</returns>
        public static JsonResult SuccessSaveResult(this Controller controller)
        {
            var result = new SaveResult
            {
                Success = true
            };

            return new JsonResult
            {
                Data = result
            };
        }

        /// <summary>
        /// Successes the save result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        /// <returns>JsonResult.</returns>
        public static JsonResult SuccessSaveResult(this Controller controller, string message)
        {
            var result = new SaveResult
            {
                Success = true,
                Message = message
            };

            return new JsonResult
            {
                Data = result
            };
        }

        /// <summary>
        /// Fails the save result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="message">The message.</param>
        /// <returns>JsonResult.</returns>
        public static JsonResult FailSaveResult(this Controller controller, string message)
        {
            var result = new SaveResult
            {
                Success = false,
                Message = message
            };

            return new JsonResult
            {
                Data = result
            };
        }

        /// <summary>
        /// Fails the save result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>JsonResult.</returns>
        public static JsonResult FailSaveResult(this Controller controller)
        {
            var result = new SaveResult
            {
                Success = false,
                Message = controller.ModelState.GetValidationSummary()
            };

            return new JsonResult
            {
                Data = result
            };
        }

        public static JsonResult SuccessSubmitResult(this Controller controller)
        {
            var result = new SubmitResult
            {
                success = true,
            };

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public static JsonResult SuccessSubmitResult(this Controller controller, string message)
        {
            var result = new SubmitResult
            {
                success = true,
                message = message
            };

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public static JsonResult SuccessSubmitResult(this Controller controller, string message, string message1)
        {
            var result = new SubmitResult
            {
                success = true,
                message = message,
                message1 = message1
            };

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Fails the save result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>JsonResult.</returns>
        public static JsonResult FailSubmitResult(this Controller controller)
        {
            var result = new SubmitResult
            {
                success = false,
                message = controller.ModelState.GetValidationSummary()
            };

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
    }
}