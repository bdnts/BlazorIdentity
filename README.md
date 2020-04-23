# BlazorIdentity

## Update 2020-04-23
Might want to check out *BlazorServerIdentityInterop.* (https://github.com/bdnts/BlazorServerIdentityInterop)   Same goals, out of the box Blazor Server with Identity scaffolded but with Razor pages.  It uses JSRuntime to get the XSRF token, dynamically create a form, and then submit the form to classic *Login* or *Logout*, where `SignInManager` actually works.  it is based on work from Shaun Walker on the Oqtane (https://oqtane.org) project.  

## Create a fully Blazor version of Identity, document the steps and issues.

Conclusion:  It can't be done with the template code during project creation.
* The files generated by scaffolding Identity are not Blazor pages.
* Login cannot be converted to Login.Razor, because SignInManager will always throw an exception regarding "The response headers cannot be modified because the response has already started."
  * This error is caused because of the differences in HTTP calls and SignalR that Blazor is built upon.  You can't put a stamp and postmark on a telephone call.  You have to use CallerId.
* I cannot find any way around this exception without significant rework
  * Build an API service to perform Login, and call it.  
  * Follow SignalR documentation to use Bearer Tokens via an Identity Server.
  
If you want to follow my notes below, possibly find a mistake I made, please, be my guess.  But I'm pretty confident that SignInManager is incompatible with Blazor, for a lot of well documented reasons.  I would love to be proven wrong.

This project is concluded.

<hr/>

Notebook

https://docs.microsoft.com/en-us/aspnet/core/security/blazor/?view=aspnetcore-3.1&tabs=visual-studio

1. Create blank blazor application 
2. Commit to Git repository public 
3. Tag Base created 
4. BlazorIdentity --> Add --> New Scaffold Item 
    1. Identity --> Add
    2. Wait for it
    3. New Layout Page – No change 
    4. Override all files –check 
    5. Data Context Class 
       1. '+
       2. BlazorIdentityContext 
       3. Add 
    6. User Class
       1.  '+ 
       2.  BlazorIdentityUser 
       3.  Add 
    7.  Add 
    8.  Wait for it  
        1.  ScaffoldingReadMe.txt 
5. ScaffoldingReadMe.txt 
   1. Startup.cs 
      1. App.UseStaticFiles() already exists 
      2. Add app.UseAuthentication() after static files 
   2. MVC 
      1. Going to re-write all the pages as Blazor/Razor pages, so no need 
   3. Database 
      1. Default is to use sql express.  I use SQL Server! (hrrumph!) 
      2. Appsettings.json 
         1. "IdentityContextConnection": "Server=<server name>;Database=BlazorIdentity;Trusted_Connection=True;MultipleActiveResultSets=true" 
         2. Will setup user secrets in a little bit 
      3. Add-migration CreateIdentitySchema 
         1. Build failed 
            1. RegisterConfirmation.cshtml.cs UserManager<> could not be found 
            2. Add using Microsoft.AspNetCore.Identity; 
            3. Successful build 
         2. Repeat add-migration CreateIdentitySchema 
            1. Success 
         3. Update-database 
            1. Success 
            2. Database Created 
         4. Commit Changes 
            1. Except appsettings.json 
6. User Secrets
   1.  Right click BlazorIdentity --> Manage User Secrets 
   2.  Transfer ConnectionStrings {} to secrets.json 
   3.  Remove extra comma if necessary 
   4.  Compile 
   5.  Debug 
   6.  Wait for it – Hello World 
   7.  Commit Changes 
       1.  Everything now 
       2.  Create tag 
7. Optional – Enable Beyond Compare for source code diffing 
    1. In Solution --> BlazorIdentity -->.git --> config 
```
[diff]
     tool = bc4 
[difftool "bc4"] 
    prompt = false 
[difftool "bc4"] 
    cmd = \"C:\\Program Files\\Beyond Compare 4\\Bcomp.exe\" \"$LOCAL\" \"$REMOTE\"  
    keepBackup = false 
[merge] 
    tool = bc4 
[mergetool "bc4"] 
    prompt = false 
[mergetool "bc4"] 
    cmd = \"C:\\Program Files\\Beyond Compare 4\\Bcomp.exe\" \"$REMOTE\" \"$LOCAL\" \"$BASE\" \"$MERGED\"  
    keepBackup = false 
    trustExitCode = true 
```
No restart necessary  

8. Reference pages
    1. https://docs.microsoft.com/en-us/aspnet/core/security/blazor/?view=aspnetcore-3.1&tabs=visual-studio 
    2. https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-3.1 
    3. https://docs.microsoft.com/en-us/aspnet/core/security/blazor/server?view=aspnetcore-3.1 
    4. https://docs.microsoft.com/en-us/aspnet/core/security/authentication/windowsauth?view=aspnetcore-3.1&tabs=visual-studio 
    5. https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-3.1&tabs=visual-studio 
    6. https://chrissainty.com/securing-your-blazor-apps-introduction-to-authentication-with-blazor/
    7. https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/
    8. https://github.com/stavroskasidis/BlazorWithIdentity
    9. https://docs.microsoft.com/en-us/aspnet/core/security/blazor/?view=aspnetcore-3.1&tabs=visual-studio#authentication
    10. https://github.com/AdrienTorris/awesome-blazor#authentication
9. BlazorApp9
   1. Created with Identity to compare against BlazorIdentity 
   2. Found many minor and a few major differences.  Hard to tell which is correct, but lean towards BlazorApp9 
   3. RevalidatingIdentityauthenticationStateProvider.cs 
      1. Pulled across file, but then needed to change namespace 
   4. Startup.cs 
      1. App.UseDatabseErrorPage not found 
         1.  ```<PackageReference Include="Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore" Version="3.1.1" />```    
         2.  Must have missed a step in the scaffolding 
             1.  ```Install-Package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore```  
10. Follow https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-3.1&tabs=visual-studio 
    1. Startup.cs 
       1. Many items noted for Startup.cs are actually in IdentityHostingStartup.cs 
          1. AddDbContext 
          2. AddDefaultIdentity 
          3. AddEntityFrameworkStores 
11. Compile and run  
    1. Success 
    2. Register 
       1. test@example.com
       2. Password1234#
       3. Acccount registered 
          1. No email confirmation yet 
       4. Account confirmed 
    3. Login 
    4. Login successful 
    5. Account confirmed in database 

### Tag Base0.1 created
12. Resuming https://docs.microsoft.com/en-us/aspnet/core/security/blazor/?view=aspnetcore-3.1&tabs=visual-studio#authentication 
13. Index.razor 
    1. Copied over the Index verbatim
    2. Successful, but dull 
```
private string message { get; set; } = "None"; 

    private async Task LogUsername() 
    { 
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync(); 
        var user = authState.User; 
  
        if (user.Identity.IsAuthenticated) 
        { 
            message = $"{user.Identity.Name} is authenticated."; 
        } 
        else 
        { 
            message = "The user is NOT authenticated."; 
        } 
    }  
```
14. App.Razor  
    1. BlazorApp wrap all components in ```<CascadingAuthorizationState>```  Documentation only wraps Layout. 
    2. I'll go with code over documentation 
### Tag Base0.2 created    

15. AuthorizeView Component - Index.razor 
    1.  Used ```<AuthorizeView>``` and ```<Authorized>``` tags to control content 
    2.  Added Roles of admin, member and family to control output 
        1.  Doesn't appear there is a RolesAdmin page.  Have to make one.  Will make it a blazor page.
        2.  Created folder Pages/Identity/Mange 
        3.  Create Roles.razor in folder 
### Tag Base0.3 created     

16.  Seed Database 
     1.   Quick method
          1.   Manually add records to AspNetRoles, AspNetUsers, and join table AspNetUserRoles 
     2.   Slow method, through OnModelCreate  
          1.   Found https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-3.1&tabs=visual-studio 
          2.   https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1 
          3.   https://stackoverflow.com/questions/59229067/how-can-i-seed-users-and-roles-in-my-asp-net-core-3-1-application 
          4.   Modify BlazorIdentityContext 
               1.   Insert 3 roles for admin, member and family. 
          5.   IdentityHostingStartup.c 
               1.   Under ```services.AddDefaultIdentity```, add ```.AddRoles<IdentityRole>();``` 
          6.   Database 
               1.   CAUTION: Migration and update-database might not work if connection string is in secrets.json.  Added it back to appsettings.json, and things started working.  Will let someone else dig into this. 
               2.   Drop the database 
               3.   Remove the migration 
               4.   Create a new migration 
               5.   Update the database (create) 
               6.   Check database that roles are present 
               7.   Run application and create user 
               8.   Manually populate AspNetUserRoles   
### Tag Base0.4 created

17. Logout error – anti forgery token exception 
    1. ```[IgnoreAntiForgeyToken]``` added to LogoutMode 
18. Admin page 
    1. Michael Washington http://blazorhelpwebsite.com/Blog/tabid/61/EntryId/4354/A-Simple-Blazor-User-and-Role-Manager.aspx 
    2. Change ```<IdentityUser>``` to ```<BlazorIdentityUser>```
    3. Change "Administration" to "admin" 
### Tag Base0.5 created

19. Roles  
    1. Changed Admin page to use Roles based authorization 
    2. Removed the direct *if* statement check, adding the admin role ```<AuthorizeView Policy="admin">``` 
### Tag Base0.6 created

20. Policy and Claims based Authorization is great, but later. 
    1. https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-3.1 
21. ```<Authorized>``` is for Webassembly, not today
22. ```[Authorize]``` is for pages only.  Will convert Administration page 
    1. Successful.  Simple message from the Router. 
    2. Changed App.razor to give innocuous message.  You don't want to confirm or deny page exists. 
23. BONUS: Added ```<AuthorizedView Roles="admin">``` around NavMenu Administration element, and it is hidden unless authorized.  Way cool. 
24. Cleaned up Index a bit. 
### Tag Base 0.7 created

25.  Blazor Login screen 
     1. LoginB 
        1. Copied Administration over to get the full monty 
        2. The key should be having the UserManager DI 
        3. Let's start with a page, and work towards a component 
        4. Logging
           1. There is a blazor extension for logging (That should be a Microsoft deliverable, not third party)
           2. https://github.com/BlazorExtensions/Logging 
           3. Will come back to this.  Commenting out for now. 
        5. Dropped all the HTML from LoginB, put ```SignInManager.PasswordSignInAsync()``` into ```OnInitializedAsync()```, to determine if this is even possible. 
        6. ERROR: "The response headers cannot be modified because the response has already started.", or some such bilge.
           1. Basically, MS Identity is dependent on HTTP as transport, so, Blazor (thankfully) is using SignalR websockets.
           2. ```SignInManager.PasswordSignInAsync()``` won't work.   MS Identity is the problem here.  Maybe I should step back into 1996 and pull out the old code to fix this! 
        7. Searching the Web, everyone has this problem, and no news in sight from MS.  Unbelievable. 
        8. Found really good explanation of error http://danpatrascu.com/manipulating-response-headers-in-asp-net-core/   
        9. This is a MAJOR architectural screw up in MS Identity. 
        10. LoginB is dead 
     2. Blazor Authentication and Authorization by Chris Sainty 
        1. Part 1 - https://chrissainty.com/securing-your-blazor-apps-introduction-to-authentication-with-blazor/ 
           1. Nice intro to Blazor Server A A, but the pages are all Asp.Net, not Blazor pages.  How to make Authentication from Blazor pages? 
        2. Part 2 - https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/ 
              1. Creates a WebAssembly app that calls Login APIs on server.  Cool, but not GA. 
     3. BlazorWithIdentity - https://github.com/stavroskasidis/BlazorWithIdentity 
        1. This could be something.  It appears they created a custom authenticator that uses MS Identity below.
        2. It uses the same api approach in WebAssembly as Chris Sainty. 
        3. Would hate to go to all this work building API if there is a simple solution LoginB error.
     4. Borrowed forms and combined logic of LoginB: 
        1. Getting same exception: "The response headers cannot be modified because the response has already started." 
        2. So close, and then to be defeated like this.   ARRRRRGGGGHHH!

 
