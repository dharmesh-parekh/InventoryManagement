namespace InventoryManagement.Web.Extensions.ExtensionModel
{
    public class SubmitResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SaveResult"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool success { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string message { get; set; }

        /// <summary>
        /// Gets or sets the message1.
        /// </summary>
        /// <value>The message.</value>
        public string message1 { get; set; }
    }
}