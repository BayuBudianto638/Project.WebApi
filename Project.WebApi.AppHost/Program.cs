var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Project_WebApi>("project-webapi");

builder.AddProject<Projects.Project_WebApi_Entities>("project-webapi-entities");

builder.AddProject<Projects.LoginServices>("loginservices");

builder.AddProject<Projects.UserManagementServices>("usermanagementservices");

builder.AddProject<Projects.CustomersServices>("customersservices");

builder.AddProject<Projects.Project_WebApi_Gateway>("project-webapi-gateway");

builder.AddProject<Projects.PalindromeServices>("palindromeservices");

builder.AddProject<Projects.MiniLMService>("minilmservice");

builder.AddProject<Projects.DocumentOCRService>("documentocrservice");

builder.Build().Run();
