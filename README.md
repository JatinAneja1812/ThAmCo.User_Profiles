# ThAmCo User Profiles WebAPI

Welcome to the ThAmCo Users WebAPI! This API is designed to manage user profiles for both customers and staff within the ThAmCo e-commerce system.

## Features in current implementation

- **Get All Customers:** Quickly fetches data for all customer profiles from the database, ensuring fast and reliable access to user information.
- **Get Staff Data:** Retrieves staff profile information based on the provided email address, offering staff users a comprehensive view of their details.
- **Add New User:** Facilitates the easy creation of new user profiles using a straightforward API endpoint, streamlining the user registration process.
- **Delete User:** Implements a user deletion mechanism based on the provided user ID, allowing for seamless removal of user profiles from the system.
- **Update User:** Enables the modification of existing user profiles using a PUT request to the API endpoint, ensuring up-to-date user information.
- **Update Customer Funds:** Implements a mechanism to update customer funds, providing flexibility in managing financial aspects related to customer profiles.
- **Direct Alter Customer Funds:** Allows staff users to directly alter customer funds, providing a convenient way to manage financial adjustments in the system.
 
## Upcoming Features

- **User Profile Customization:** Allow users to customize their profiles with additional information and preferences, enhancing the user experience.
- **Activity Logging:** Implement comprehensive activity logging to track user interactions and system events for improved auditing.
- **User Notifications:** Introduce a notification system to keep users informed about important updates, such as order status changes and account activities.
- **Role-Based Access Control (RBAC):** Implement a role-based access control system for staff profiles, allowing for fine-grained control over user permissions.
- **User Profile Deactivation:** Introduce the ability to deactivate user profiles without permanent deletion, providing an alternative to account removal.
- **User Analytics Dashboard:** Develop a user analytics dashboard for administrators to gain insights into user engagement, trends, and system usage.

## Getting Started
Follow these steps to set up and run the ThAmCo User Profiles WebAPI on your local machine.

### Prerequisites
Before you can run the ThAmCo User Profiles WebAPI project in Visual Studio 2022, ensure that you have the following prerequisites installed on your development environment:

1. **Visual Studio 2022:** 
   - You can download the latest version of Visual Studio 2022 from the [official Visual Studio website](https://visualstudio.microsoft.com/downloads/).

2. **.NET 6 SDK:**
   - The project is built using .NET 6, and you'll need the .NET 6 SDK. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/6.0).

3. **Database:**
   - To connect to you database locally ensure you have SQL Server 2019 and Microsoft SQL Server Managment Studio installed:
   - Link:[Microsoft SQL Server Managment Studio](https://sqlserverbuilds.blogspot.com/2018/01/sql-server-management-studio-ssms.html) 
   - Link:[SQL Server 2019](https://www.microsoft.com/en-us/evalcenter/download-sql-server-2019)

### Installation

1. Fork the project.
2. Open Visual Studio 2022 Community/Professional/Enterprise Edition in Admin Mode.
3. Clone the project by coping the link from GitHub repository.
4. Make a new branch from master.
5. Right click on ThAmCo.User_Profiles and make it as default startup project ( Click on set as startup project button in the menu).
6. Clean the project. Click build(On top of IDE) -> Clean Solution.
7. Click on ThAmCo.User_Profiles on top navigation bar or either click on "Play" button to run the project.

If there is error connecting to your local database make sure you have installed correct version of SQL Server and SQL Server Managment Studio is connected. 
Also in SQL Server Managment Studio : Database Engine name is : Localhost\\SQLExpress.

## Connected Projects 
- ThAmCo.Order_Management.WebAPI :  Link:[here](https://github.com/JatinAneja1812/ThAmCo.Order_Management.WebAPI).
- ThAmCo.Staff_Portal.WebAPI :      Link:[here](https://github.com/JatinAneja1812/ThAmCo.Staff_Protal_BFF.WebAPI).
- thamco_staff_frontend :           Link:[here](https://github.com/JatinAneja1812/thamco_staff_frontendapp).
