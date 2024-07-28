/**************************************************************************************
File name: Form1.cs
Description: Creates a GUI for interacting with recycler data, enabling reading, 
             displaying, editing, saving, sorting, and searching of CSV data.
Version: 1.0.0
Author: ┬  ┬ ┬┬┌─┌─┐┬ ┬┌─┐╦╔╦╗
        │  │ │├┴┐├┤ │││├─┤║ ║
        ┴─┘└─┘┴ ┴└─┘└┴┘┴ ┴╩ ╩
Date: October 24, 2024
License: MIT License

Dependencies:
.NET Core 3.1
Visual Studio 2019/2022

GitHub Repository: https://github.com/LukeWait/recycler-app
**************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RecyclerContacts
{

    public partial class Form1 : Form
    {
        // recyclerList of recycler data stored in recyclers.csv
        private List<Recycler> recyclerList;
        // current selected record (index value)
        private int currentRecord;


        /****************************************************************************
        Method:     Form1()
        Purpose:    Constructs GUI components
                    Initialises private data
                    Reads in external csv files
                    Sets up recyclers in recyclerList
                    Sets up waste types in combobox
                    Sorts recyclerList by business name
                    Displays filtered recyclerList in the listingTextBox
        Input:      void
        Output:     Constructor method/no return value
        ****************************************************************************/
        /// <summary>
        /// Constructor method
        /// </summary>
        public Form1()
        {
            // Visual Studio GUI setup
            InitializeComponent();

            // instantiate the recyclerList
            recyclerList = new List<Recycler>();
            // instantiate current record
            currentRecord = 0;

            // read external file data and set up recyclerList
            ReadRecyclerData("data/recyclers.csv");
            // read external file data and set up filterComboBox
            ReadComboData("data/wastelist.csv");
            
            // display recyclers read in from external file
            if (recyclerList.Count > 0)
            {
                // display recycler data under Recycler Listing
                DisplayRecyclerData();
                // display recycler data under Local Recycler Contacts
                DisplayCurrentRecord();
            }
            // display pop-up indicating that no records exist
            else
            {
                MessageBox.Show("There are no recycler records to display", "NO RECORDS!");
            }
 
        }// end constructor method


        /****************************************************************************
        Method:     ReadRecyclerData()
        Purpose:    Reads recyclers.csv and creates instance of Recycler class for
                    every line, then addes instance to recyclerList
        Input:      string filePath (name of external csv to be read)
        Output:     void
        ****************************************************************************/
        /// <summary>
        /// ReadRecyclerData() method
        /// </summary>
        /// <param name="filePath">Name of external csv to be read</param>
        public void ReadRecyclerData(string filePath)
        {
            try
            {
                // if file exists
                if (File.Exists(filePath))
                {
                    // Read file line by line using StreamReader
                    using (StreamReader file = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = file.ReadLine()) != null)
                        {
                            // split the line into a string array
                            string[] lineArray = line.Split(',');
                            // assign array values to string vars
                            string business = lineArray[0];
                            string address = lineArray[1];
                            string phone = lineArray[2];
                            string website = lineArray[3];
                            string recycles = lineArray[4];
                            // create Recycler instance
                            Recycler recycler = new Recycler(business, address, phone, website, recycles);
                            // add instance to recyclerList
                            recyclerList.Add(recycler);
                        }

                        // close the reader
                        file.Close();

                        // sort the recyclerList by alpha asc
                        recyclerList.Sort();

                    }// end using StreamReader file
                }
                // display in pop-up that file does not exist
                else
                {
                    MessageBox.Show("ERROR: No external file exists for: " + filePath, "ERROR!");
                }
            }
            // display exceptions as pop-up messages
            catch (IOException ioe)
            {
                MessageBox.Show("ERROR: Problem in reading the external file: " + filePath + "\r\n"
                    + ioe.Message, "ERROR!");
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Problem with external file: " + filePath + "\r\n"
                    + e.Message, "ERROR!");
            }

        }// end ReadRecyclerData()


        /****************************************************************************
        Method:     ReadComboData()
        Purpose:    Reads wastelist.csv and adds data to filterComboBox
        Input:      string filePath (name of external csv to be read)
        Output:     void
        ****************************************************************************/
        /// <summary>
        /// ReadComboData() method
        /// </summary>
        /// <param name="filePath">Name of external csv to be read</param>
        public void ReadComboData(string filePath)
        {
            // clear out any existing items in the combo box
            filterComboBox.Items.Clear();

            try
            {
                // if file exists
                if (File.Exists(filePath))
                {
                    // Read file line by line using StreamReader
                    using (StreamReader file = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = file.ReadLine()) != null)
                        {
                            // add each line to the combobox
                            filterComboBox.Items.Add(line);
                        }

                        // close the reader
                        file.Close();

                    }// end using StreamReader file
                }
                // display in pop-up that file does not exist
                else
                {
                    MessageBox.Show("ERROR: No external file exists for: " + filePath, "ERROR!");
                }
            }
            // display exceptions as pop-up messages
            catch (IOException ioe)
            {
                MessageBox.Show("ERROR: Problem in reading the external file: " + filePath + "\r\n"
                    + ioe.Message, "ERROR!");
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Problem with external file: " + filePath + "\r\n"
                    + e.Message, "ERROR!");
            }

        }// end DisplayComboData()


        /****************************************************************************
        Method:     DisplayRecyclerData()
        Purpose:    Displays recyclerList data in listingTextBox
                    Filters displayed items by filterComboBox selected item
        Input:      void
        Output:     void
        ****************************************************************************/
        /// <summary>
        /// DisplayRecyclerData() method
        /// </summary>
        public void DisplayRecyclerData()
        {
            // format heading string
            String displayText = "Name\t\t\tPhone\t\tWeb Site\r\n";
            displayText += "-------------------------------------------------------------------------" +
             "---------------------------------------------------------------------------------------\r\n";

            // if combobox is "All" or "" (occurs during constructor), display every record
            if (filterComboBox.Text.Equals("All") || filterComboBox.Text.Equals(""))
            {
                // display each formatted string for every recycler
                for (int i = 0; i < recyclerList.Count(); i++)
                {
                    displayText += recyclerList[i].ToString() + "\r\n";
                }
            }
            // display only records that contain the combobox selection
            else
            {
                // linear search through the list
                for (int i = 0; i < recyclerList.Count; i++)
                {
                    // when recycles contains selected combobox item
                    if (recyclerList[i].Recycles.Contains(filterComboBox.Text))
                    {
                        displayText += recyclerList[i].ToString() + "\r\n";         
                    }                   
                }     
            }

            // set accumlated details to the textbox
            listingTextBox.Text = displayText;

        }// end DisplayRecyclerData()


        /****************************************************************************
        Method:     DisplayCurrentRecord()
        Purpose:    Sets recycler details to textboxes under Local Recycler Contacts
                    according to currentRecord value
        Input:      void
        Output:     void
        ****************************************************************************/
        /// <summary>
        /// DisplayCurrentRecord() method
        /// </summary>
        public void DisplayCurrentRecord()
        {
            businessTextBox.Text = recyclerList[currentRecord].Business;
            addressTextBox.Text = recyclerList[currentRecord].Address;
            phoneTextBox.Text = recyclerList[currentRecord].Phone;
            websiteTextBox.Text = recyclerList[currentRecord].Website;
            recyclesTextBox.Text = recyclerList[currentRecord].Recycles;

        }// end DisplayCurrentRecord()


        /****************************************************************************
        Method:     IsRecyclerValid()
        Purpose:    Performs validation checks on textboxes under Local Recycler Contacts
        Input:      void
        Output:     bool (must pass all validation checks to be true)
        ****************************************************************************/
        /// <summary>
        /// IsRecyclerValid() method
        /// </summary>
        /// <returns>bool indicating whether all validation checks have passed</returns>
        public bool IsRecyclerValid()
        {
            // return validate, changes to false is any checks fail
            bool enrolmentStatus = true;
            // string for accumulated error msg
            string errorMessage = "ERROR(S) encountered\n";

            // check business name
            if (String.IsNullOrEmpty(businessTextBox.Text))
            {
                errorMessage += "Business name is required\n";
                enrolmentStatus = false;
            }
            // check address
            if (String.IsNullOrEmpty(addressTextBox.Text))
            {
                errorMessage += "Address is required\n";
                enrolmentStatus = false;
            }
            // check phone
            if (String.IsNullOrEmpty(phoneTextBox.Text))
            {
                errorMessage += "Phone is required\n";
                enrolmentStatus = false;
            }
            // check website
            if (String.IsNullOrEmpty(websiteTextBox.Text))
            {
                errorMessage += "Website is required\n";
                enrolmentStatus = false;
            }
            // check recycles
            if (String.IsNullOrEmpty(recyclesTextBox.Text))
            {
                errorMessage += "Recycles is required\n";
                enrolmentStatus = false;
            }

            // display error message if errors encountered
            if (enrolmentStatus == false)
            {
                MessageBox.Show(errorMessage, "ERRORS!");
            }

            // return false if any errors encountered
            return enrolmentStatus;

        }// end IsRecyclerValid()


        /****************************************************************************
        Method:     firstButton_Click() event handler
        Purpose:    Called when firstButton is clicked
                    Changes currentRecord to 0 and calls DisplayCurrentRecord()
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void firstButton_Click(object sender, EventArgs e)
        {
            // go to the first record in the recyclerList
            currentRecord = 0;

            // display this record in the Local Recycler Contacts section
            DisplayCurrentRecord();

        }// end firstButton event


        /****************************************************************************
        Method:     previousButton_Click() event handler
        Purpose:    Called when previousButton is clicked
                    Decrements currentRecord and calls DisplayCurrentRecord()
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void previousButton_Click(object sender, EventArgs e)
        {
            // go to the previous record in the recyclerList
            currentRecord--;
            // check if currentRecord not less than zero
            // if so, then assign to last element index
            // (or Count – 1) of the recyclerList 
            // this being the last record
            if (currentRecord < 0)
            {
                currentRecord = recyclerList.Count - 1;
            }
            // display this record in the Local Recycler Contacts section
            DisplayCurrentRecord();

        }// end previousButton event


        /****************************************************************************
        Method:     nextButton_Click() event handler
        Purpose:    Called when nextButton is clicked
                    Increments currentRecord and calls DisplayCurrentRecord()
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void nextButton_Click(object sender, EventArgs e)
        {
            // go to the next record in the recyclerList
            currentRecord++;
            // check if currentRecord not greater than the list length - 1
            // if so, then assign to first element index [0] 
            // of the recyclerList 
            // this being the first record
            if (currentRecord == recyclerList.Count)
            {
                currentRecord = 0;
            }
            // display this record in the Local Recycler Contacts section
            DisplayCurrentRecord();

        }// end nextButton event


        /****************************************************************************
        Method:     lastButton_Click() event handler
        Purpose:    Called when lastButton is clicked
                    Changes currentRecord to last index of recyclerList and 
                    calls DisplayCurrentRecord()
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void lastButton_Click(object sender, EventArgs e)
        {
            // go to the last record in the recyclerList
            currentRecord = recyclerList.Count - 1;

            // display this record in the Local Recycler Contacts section
            DisplayCurrentRecord();

        }// end lastButton event


        /****************************************************************************
        Method:     urlButton_Click() event handler
        Purpose:    Called when urlButton is clicked
                    Opens the default browser to the url of the currently displayed
                    recycler instance
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void urlButton_Click(object sender, EventArgs e)
        {
            // open in browser and display error if fails
            try
            {
                Process.Start("explorer", websiteTextBox.Text);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

        }// end urlButton event


        /****************************************************************************
        Method:     binarySearchButton_Click() event handler
        Purpose:    Called when binarySearchButton is clicked
                    Uses BinarySearch() method to return index value
                    If findTextBox matches existing record, changes currentRecord
                    to the returned index value and calls DisplayCurrentRecord()
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void binarySearchButton_Click(object sender, EventArgs e)
        {
            // string of the search item (business name)
            string businessToSearch = findTextBox.Text;

            // check if the search text box is not empty
            if (String.IsNullOrEmpty(businessToSearch))
            {
                MessageBox.Show("Business name is required in the search field", "ERROR!");
                return;
            }
            else
            {
                // use BinarySearch to return index value if record exists
                // otherwise returns a negative number that is the bitwise
                // complement of the index in relation to sort order
                int foundIndex = recyclerList.BinarySearch(new Recycler(businessToSearch, "", "", "", ""));

                if (foundIndex >= 0)
                {
                    // if found, change currentRecord to the index
                    currentRecord = foundIndex;
                    // display the found record
                    DisplayCurrentRecord();
                    // inform the user that the record was found
                    MessageBox.Show(businessToSearch + " found at index " + foundIndex, "FOUND!");
                }
                else
                {
                    // inform the user that the record was NOT found
                    MessageBox.Show(businessToSearch + " NOT found!", "NOT FOUND");
                }
            }

        }// end binarySearchButton event


        /****************************************************************************
        Method:     clearButton_Click() event handler
        Purpose:    Called when clearButton is clicked
                    Empties all textboxes under Local Recycler Contacts
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void clearButton_Click(object sender, EventArgs e)
        {
            businessTextBox.Text = "";
            addressTextBox.Text = "";
            phoneTextBox.Text = "";
            websiteTextBox.Text = "";
            recyclesTextBox.Text = "";

        }// end clearButton event


        /****************************************************************************
        Method:     addNewButton_Click() event handler
        Purpose:    Called when addNewButton is clicked
                    Calls IsRecyclerValid to validate data fields
                    If returned value is true, creates a new instance of Recycler
                    class and adds to recyclerList
                    Calls recycler.Sort() to reorder recyclerList with new entry
                    Calls DisplayRecyclerData() to display new instance in listingTextBox
                    Changes currentRecord to reflect index of new recycler in regards
                    to the newly sorted recyclerList
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void addNewButton_Click(object sender, EventArgs e)
        {
            // call IsRecyclerValid() to validate all Local Recycler Contracts textboxes
            if (IsRecyclerValid() == false)
            {
                return;
            }
            else
            {
                // prompt the user to proceed with the save
                DialogResult dialogResult = MessageBox.Show("Do you wish " +
                    "to proceed in adding this new recycler record ? ", "NEW RECYCLER" +
                    "RECORD", MessageBoxButtons.YesNo);

                // if Yes button clicked, then proceed
                if (dialogResult == DialogResult.Yes)
                {
                    // get all 5 values for the new recycler
                    string business = businessTextBox.Text;
                    string address = addressTextBox.Text;
                    string phone = phoneTextBox.Text;
                    string website = websiteTextBox.Text;
                    string recycles = recyclesTextBox.Text;
                    // create new recycler object
                    Recycler newRecycler = new Recycler(business, address, phone, website, recycles);
                    // add to the recyclerList
                    recyclerList.Add(newRecycler);
                    // re-sort the recyclerList
                    recyclerList.Sort();
                    // display newly sorted recycler list
                    DisplayRecyclerData();
                    // get index number of new recycler
                    currentRecord = recyclerList.IndexOf(newRecycler);
                }
            }

        }// end addNewButton event


        /****************************************************************************
        Method:     updateButton_Click() event handler
        Purpose:    Called when updateButton is clicked
                    Calls IsRecyclerValid to validate data fields
                    If returned value is true, updates recyclerList entry at
                    currently selected index with all 5 data fields
                    Calls DisplayRecyclerData() to update listingTextBox
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void updateButton_Click(object sender, EventArgs e)
        {
            // call IsRecyclerValid() to validate all Local Recycler Contracts textboxes
            if (IsRecyclerValid() == false)
            {
                return;
            }
            else
            {
                // prompt the user to proceed with the save
                DialogResult dialogResult = MessageBox.Show("Do you wish " +
                    "to proceed in updating the record for " + recyclerList[currentRecord].Business + " ? ",
                    "UPDATE EXISTING RECYCLER RECORD", MessageBoxButtons.YesNo);

                // if Yes button clicked, then proceed
                if (dialogResult == DialogResult.Yes)
                {
                    // update recyclerList of selected index with all 5 values
                    recyclerList[currentRecord].Business = businessTextBox.Text;
                    recyclerList[currentRecord].Address = addressTextBox.Text;
                    recyclerList[currentRecord].Phone = phoneTextBox.Text;
                    recyclerList[currentRecord].Website = websiteTextBox.Text;
                    recyclerList[currentRecord].Recycles = recyclesTextBox.Text;

                    // display newly updated recycler info
                    DisplayRecyclerData();
                }
            }

        }// end updateButton event


        /****************************************************************************
        Method:     deleteButton_Click() event handler
        Purpose:    Called when deleteButton is clicked
                    Removes recyclerList entry at selected index
                    Sets newly selected recycler (due to index change) to data fields
                    Calls DisplayRecyclerData() to remove from listingTextBox
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // prompt the user to proceed with the delete
            DialogResult dialogResult = MessageBox.Show("Do you wish " +
                "to proceed in deleting the record for " + recyclerList[currentRecord].Business + " ? ",
                "DELETE EXISTING RECYCLER RECORD", MessageBoxButtons.YesNo);

            // if Yes button clicked, then proceed
            if (dialogResult == DialogResult.Yes)
            {
                // remove entry from recyclerList
                recyclerList.RemoveAt(currentRecord);
                
                // check if deleting last record to avoid index out of bounds
                if(currentRecord == recyclerList.Count)
                {
                    currentRecord = 0;
                }
                // set all 5 values for new currentRecord
                businessTextBox.Text = recyclerList[currentRecord].Business;
                addressTextBox.Text = recyclerList[currentRecord].Address;
                phoneTextBox.Text = recyclerList[currentRecord].Phone;
                websiteTextBox.Text = recyclerList[currentRecord].Website;
                recyclesTextBox.Text = recyclerList[currentRecord].Recycles;

                // display newly modified recycler list
                DisplayRecyclerData();
            }

        }// end deleteButton event


        /****************************************************************************
        Method:     exitButton_Click() event handler
        Purpose:    Called when exitButton is clicked
                    Calls the DisplayRecyclerData() method to display filtered
                    listingTextBox based on filterComboBox selected item
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void exitButton_Click(object sender, EventArgs e)
        {
            // show pop up with yes/no option to save
            DialogResult result = MessageBox.Show("You are about to exit the " +
                "application - do you wish to SAVE changes to external file?", "SAVE!",
                    MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // save all records from the list
                if (recyclerList.Count > 0)
                {
                    // save to external file
                    try
                    {
                        string filePath = @"recyclers.csv";
                        // StreamWriter object
                        StreamWriter sw = new StreamWriter(filePath);                       
                        // loop through each instance 
                        // and write a line for each instance
                        for (int i = 0; i < recyclerList.Count; i++)
                        {
                            sw.WriteLine(recyclerList[i].ToCSVString());
                        }
                        //Close the file
                        sw.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: Problem in writing to external file!\r\n"
                            + ex.Message, "ERROR!");
                    }
                }
            }
            // exit the application
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }

        }// end exitButton event


        /****************************************************************************
        Method:     filterComboBox_SelectedIndexChanged() event handler
        Purpose:    Called when a selection is made from the filterComboBox
                    Calls the DisplayRecyclerData() method to display filtered
                    listingTextBox based on filterComboBox selected item
        Input:      object sender (the button instance)
                    EventArgs e (event generated data)
        Output:     void
        ****************************************************************************/
        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayRecyclerData();

        }// end filterComoBox event

    }//end Form1()
}
