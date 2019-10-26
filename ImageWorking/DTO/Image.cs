namespace ImageWorking.DTO
{
    /// <summary>
    /// DTO to store information about image
    /// </summary>
    public class Image<Type>
    {

        /// <summary>
        /// Stores path to current image
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Stores content of current immage
        /// </summary>
        public Type Content { get; set; }
    }
}
