namespace ElectronicLibrary.Core.Entity
{
    public class ErrorEntity
    {
        public ErrorEntity(string userMessage, string devMessage)
        {
            this.userMessage = userMessage;
            this.devMessage = devMessage;
        }

        /// <summary>
        /// Lỗi thông báo đến người dùng
        /// </summary>
        public string userMessage { get; set; }
        /// <summary>
        /// Lỗi thông báo cho lập trình viên
        /// </summary>
        public string devMessage { get; set; }
    }
}
