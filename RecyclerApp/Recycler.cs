/****************************************************************************
File name: Recycler.cs
Purpose:   1. Represents an instance of a recycler business
           2. Contains private variables accessable with public methods
           3. Implements IComparable interface
           4. Contains method overload of ToString()
           5. Contains method to return csv formatted string
           6. Contains CompareTo() method to compare Recycler instances
              by business name
Author:    Luke Wait
Date:      24.10.22
Version:   1.0
Notes:     
****************************************************************************/

using System;

namespace RecyclerApp
{

    /// <summary>
    /// Recycler class
    /// Purpose:    Provide class template for recycler data
    /// Implements: IComparable interface and CompareTo() method
    /// </summary>
    /// <remarks>Luke Wait
    ///          24.10.22
    ///          Version 1.0</remarks>
    class Recycler : IComparable<Recycler>
    {
        // private variables with public get and set methods
        /// <summary>
        /// Public property: Business (name of the recycler)
        /// </summary>
        public string Business { get; set; }
        /// <summary>
        /// Public property: Address (address of the recycler)
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Public property: Phone (phone contact of the recycler)
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Public property: Website (url of the recycler)
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Public property: Recycles (types of waste recycled)
        /// </summary>
        public string Recycles { get; set; }


        /****************************************************************************
        Method:     Recycler() --- contructor method with inputs
        Purpose:    Creates a new recycler instance
        Input:      string business --- name of the recycler
                    string address  --- address of the recycler
                    string phone    --- phone contact of the recycler
                    string website  --- url of the recycler
                    string recycles --- types of waste recycled
        Output:     Constructor method/no return type
        ****************************************************************************/
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="business">Name of the recycler</param>
        /// <param name="address">Address of the recycler</param>
        /// <param name="phone">Phone contact of the recycler</param>
        /// <param name="website">Url of the recycler</param>
        /// <param name="recycles">Types of waste recycled</param>
        public Recycler(string business, string address, string phone, string website, string recycles)
        {
            Business = business;
            Address = address;
            Phone = phone;
            Website = website;
            Recycles = recycles;
        }// end Recycler() constructor


        /****************************************************************************
        Method:     ToString()
        Purpose:    Compiles a formatted string from the Recycler instance data for 
                    display in the listingTextBox
        Input:      void
        Output:     Formatted string for display in listingTextBox
        ****************************************************************************/
        /// <summary>
        /// Overridden ToString() method
        /// </summary>
        /// <returns>Formatted string for display in listingTextBox</returns>
        public override string ToString()
        {
            string recyclerString = Business + "\t" + Phone + "\t" + Website;

            return recyclerString;

        }// end ToString()


        /****************************************************************************
        Method:     ToCSVString()
        Purpose:    Compiles a string with appropriate instance data and formatting
                    to be written to external file recycler.csv
        Input:      void
        Output:     CSV formatted string for writing to recycler.csv
        ****************************************************************************/
        /// <summary>
        /// ToCSVString() method
        /// </summary>
        /// <returns>CSV formatted string for writing to recycler.csv</returns>
        public string ToCSVString()
        {
            return Business + "," + Address + "," + Phone + "," + Website +
           "," + Recycles;

        } // end ToCSVString()


        /****************************************************************************
        Method:     CompareTo()
        Purpose:    Compares the business of the Recycler instance with the business of another
                    Used for sorting instances (with Sort() method)
                    Implemented method from IComparable interface
        Input:      Recycler object (other instance being compared)
        Output:     int
                    Returns 0 if both business names are equal
                    Returns -1 if this.Business is alphabetically less than other.Business
                    Returns 1 if this.Business is alphabetically greater than other.Business
        ****************************************************************************/
        /// <summary>
        /// Implemented CompareTo() method
        /// </summary>
        /// <param name="other">Recycler object (other instance being compared)</param>
        /// <returns>Returns 0 if both business names are equal,
        ///          Returns -1 if this.Business is alphabetically less than other.Business,
        ///          Returns 1 if this.Business is alphabetically greater than other.Business</returns>
        public int CompareTo(Recycler other)
        {
            return this.Business.CompareTo(other.Business);

        }// end CompareTo()
    }
}
