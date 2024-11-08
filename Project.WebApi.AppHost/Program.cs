var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Project_WebApi>("project-webapi");

builder.AddProject<Projects.Project_WebApi_Entities>("project-webapi-entities");

builder.AddProject<Projects.LoginServices>("loginservices");

builder.AddProject<Projects.UserManagementServices>("usermanagementservices");

builder.AddProject<Projects.CustomersServices>("customersservices");

builder.AddProject<Projects.Project_WebApi_Gateway>("project-webapi-gateway");

builder.Build().Run();
