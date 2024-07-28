# Recycler Contacts App
## Description
A Windows Forms application for managing recycling business data, featuring CSV manipulation for CRUD operations, binary search, and filtering.

<p align="center">
  <img src="https://github.com/LukeWait/recycler-contacts-app/raw/main/RecyclerContacts/images/recycler-contacts-app-screenshot.png" alt="App Screenshot" width="600">
</p>

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Development](#development)
- [License](#license)
- [Acknowledgments](#acknowledgments)
- [Source Code](#source-code)
- [Dependencies](#dependencies)

## Installation
This app is designed for Windows only. It leverages the .NET Core 3.1 framework, which ensures modern, efficient, and secure runtime behavior. Requirements include:
- **Executable:** The .NET Core 3.1 runtime must be installed on the user's system.
- **From Source:** Development tools including the .NET Core 3.1 SDK and Visual Studio 2019 (v16.4+) or later versions are necessary.

### Executable
1. Download the latest release from the [releases page](https://github.com/LukeWait/recycler-contacts-app/releases).
2. Extract the contents to a desired location.
3. Navigate to the `bin` directory.
4. Run the `RecyclerContacts.exe` file.

### From Source
To install and run the application from source:
1. Clone the repository:
    ```sh
    git clone https://github.com/LukeWait/recycler-contacts-app.git
    cd recycler-contacts-app
    ```
2. Open the solution file `RecyclerContactsApp.sln` in Visual Studio.
3. Restore NuGet packages if prompted.
4. Build or run the solution using Visual Studio.

## Usage
1. Launch the application by running the `RecyclerContacts.exe` or from source.
2. Use the GUI to manipulate the CSV files using the options provided by the interface.

### Application Functions
- **Navigate Contacts Buttons**: Move forward and backward through contacts, or jump to first or last.
- **Clear Fields**: Reset all input fields to their default state.
- **Add New Record**: Input new recycler contact information and save it to the CSV file.
- **Update Existing Record**: Modify details of an existing recycler contact and save the changes.
- **Delete Existing Record**: Remove a recycler contact from the CSV file.
- **Binary Search by Business Name**: Quickly find a recycler contact using binary search.
- **Filter Recycler Contacts**: Display only the recycler contacts that deal with specific waste types.
- **Link to Recycler Websites**: Access the websites of recyclers directly through the application.

## Development
This project was developed using Visual Studio and utilizes Windows Forms. For the best experience with Windows Forms projects that involve extensive use of the Form Designer, it is recommended to use Visual Studio as it will be the most reliable and feature-complete IDE option.
If you are exploring alternatives and willing to handle some design tasks manually or switch between IDEs, JetBrains Rider on Windows could be a viable option, but it might not offer the same level of designer support as Visual Studio.

### Project Structure
```sh
recycler-contacts-app/
├── data/                              # CSV files for persistent data storage
├── RecyclerContacts/
│   ├── images/                        # GUI design elements
│   ├── Properties/                    # Project properties
│   ├── Form1.Designer.cs              # Main form designer code
│   ├── Form1.cs                       # Main form code
│   ├── Form1.resx                     # Main form resources
│   ├── Program.cs                     # Main program entry point
│   ├── Recycler.cs                    # Class representing an instance of a Recycler business
│   ├── RecyclerContacts.csproj        # Project file
│   └── RecyclerContacts.xml           # XML documentation for classes and methods
└── RecyclerContactsApp.sln            # Visual Studio Solution file

```

### Data Storage
The application provides persistent storage by utilizing two CSV files: `recyclers.csv` and `wastelist.csv`.

#### `recyclers.csv`
This file contains the data for recycler contacts which is editable through the app. It includes the following columns:
<table>
  <tr>
    <th style="border-right:1px solid black">Business Name</th>
    <th style="border-right:1px solid black">Address</th>
    <th style="border-right:1px solid black">Phone</th>
    <th style="border-right:1px solid black">Website</th>
    <th>Recycles</th>
  </tr>
  <tr>
    <td style="border-right:1px solid black">Gold Coast City Council</td>
    <td style="border-right:1px solid black">833 Southport Nerang Road Nerang QLD 4211</td>
    <td style="border-right:1px solid black">07-55-828-211</td>
    <td style="border-right:1px solid black">https://www.goldcoast.qld.gov.au/Services/Waste-recycling/Recycling-services</td>
    <td>Electronic Waste - Household Waste - Hazardous Waste - Unwanted Items</td>
  </tr>
  <tr>
    <td style="border-right:1px solid black">Adrians Metal Recyclers</td>
    <td style="border-right:1px solid black">14 Manufacturer Drive Molendinar QLD 4214</td>
    <td style="border-right:1px solid black">07-55-646-866</td>
    <td style="border-right:1px solid black">https://adrians.com.au/</td>
    <td>Scrap Metal - Scrap Cars</td>
  </tr>
</table>

#### `wastelist.csv`
This file lists waste types for filtering recycler contacts. If additional filter options are desired, they must be manually added to this list.
<table>
  <tr>
    <td>All</td>
  </tr>
  <tr>
    <td>Electronic Waste</td>
  </tr>
  <tr>
    <td>Green Waste</td>
  </tr>
  <tr>
    <td>Hazardous Waste</td>
  </tr>
  <tr>
    <td>Household Waste</td>
  </tr>
  <tr>
    <td>Scrap Cars</td>
  </tr>
  <tr>
    <td>Scrap Metal</td>
  </tr>
  <tr>
    <td>Unwanted Items</td>
  </tr>
</table>

### Creating New Releases
- **Build the Application**: Use Visual Studio to compile and package the application into an executable (.exe) file. Ensure that all dependencies and necessary files are included in the build process.
  - Open the solution file (`RecyclerContactsApp.sln`) in Visual Studio.
  - Restore any NuGet packages if prompted.
  - Build the solution by selecting `Build` > `Build Solution` from the menu.
  - Locate the compiled `.exe` file in the `bin/Release` or `bin/Debug` directory, depending on your build configuration.

- **Data Files**: Ensure that the CSV files `recyclers.csv` and `wastelist.csv` are included in the build. The current project settings will include them in the `data` directory.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgments
This project was developed as part of an assignment at TAFE Queensland for subject ICTPRG443.

Project requirements and initial GUI design/codebase provided by Hans Telford.

## Source Code
The source code for this project can be found in the GitHub repository: [https://github.com/LukeWait/recycler-contacts-app](https://www.github.com/LukeWait/recycler-contacts-app).

## Dependencies
- Windows 10 or later
- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Visual Studio 2019 (v16.4+)](https://visualstudio.microsoft.com/vs/) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
