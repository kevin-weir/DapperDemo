The Folder structure in the database project should closely follow the conventions used in SQL Server Studio or SQL Server Object explorer
  ie.  dbo
         Tables
         Views
         Stored Procedures
         Views
         etc

Refactor
  Change Column Name and it can fix everywhere
  In SQL SELECT expand * (wildcard) to actual column names

Create publish file and save in PublishLocations folder
  Use convention such as Development_DatabaseName ie.  Development_Dapper.publish.xml

Create a compare to compare target database with your project.  Save the compare in a new Compare folder
  Use convention such as Development_DatabaseName ie.  Development_Dapper.scmp

Create Pre and Post Deployment scripts and store them in new folders PreDeploymentScripts and PostDeploymentScripts respectively
  Scripts are created by right clicking on database project and selecting User Scripts first.  The references to these files are stored
  in the database project file so even if you move and remame them it will remember each files purpose.
  Naming convention of Script.PreDeployment.sql and Script.PostDeployment.sql

In SQL Server object explorer you can right click database and then select Data Comparison to run a data compare between a source and target database.


