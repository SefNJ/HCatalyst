namespace HCatalyst.Models
{
    /// <summary>
    /// The fields for the Person information stored within the application.
    /// </summary>
    public class HCperson
    {
        /// <summary>
        /// Primary key value
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// FIrst Name of the person.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name of the person.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The current street name and number of the person.
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// The city where the person resides.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// THe state where the person resides.
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// The zip code where the person resides.
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// The persons's age.
        /// </summary>
        public short Age { get; set; }
        /// <summary>
        /// The commas sepereated values that note the interests of the person.
        /// </summary>
        public string Interests { get; set; }

    }

    /// <summary>
    /// Used passing parameter in the query string
    /// </summary>
    public class HCparameter
    {
        /// <summary>
        /// The Delay query string is the number of seconds the response will be delayed.  Its range is from 0 to 255.
        /// </summary>
        public byte Delay { get; set; } 
    }
}