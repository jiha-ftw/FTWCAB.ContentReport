## Optimizely CMS 12 Content Report

```
Install-Package FTWCAB.ContentReport
```

Install and navigate to Add-ons / Content Usages within CMS.
Pick a content type and see instances of the selected type as well as usages of those instances.

### Version history

#### 0.2.10
* Language selector

#### 0.2.9
* Possible to set which role(s) should have access to the module.**<br>
eg `services.ConfigureContentReport(o => o.SetAccessRoles("CmsAdmins", "CmsEditors"));`**<br>
If not run, the default role is `CmsAdmins`.

#### 0.2.8
* Optimize content type usage listing

#### 0.2.7
* Adapt targets file to work/include zip file to modules\_protected directory when publishing

### Overview, listing instances of a selected content type
![Overview for selected content type](https://www.ftwconsulting.se/img/scr3.png)

### Usages of a specific instance of the content type
![Usages of a specific instance of the content type](https://www.ftwconsulting.se/img/scr4.png)
