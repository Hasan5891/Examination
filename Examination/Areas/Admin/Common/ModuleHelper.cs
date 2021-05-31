using Microsoft.AspNetCore.Mvc;
using Examination.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace Examination.Areas.Admin.Common.Extensions
{
    /// <summary>
    /// This is where you customize the navigation sidebar
    /// </summary>
    public static class ModuleHelper
    {
        public enum Module
        {
            Home,
            About,
            Contact,
            Error,
            Login,
            Register,
            SuperAdmin,
            Role,
            UserLogs,
            Test,
            Predmet,
            Part,
            Result,
            Localhost,
            Site


        }

        public static SidebarMenu AddHeader(string name)
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Header,
                Name = name,
            };
        }

        public static SidebarMenu AddTree(string name, string iconClassName = "fa fa-link")
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Tree,
                IsActive = false,
                Name = name,
                IconClassName = iconClassName,
                URLPath = "#",
            };
        }

        public static SidebarMenu AddModule(Module module, Tuple<int, int, int> counter = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case Module.Home:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Testlar",
                        IconClassName = "fa fa-link",
                        URLPath ="/Admin/Home",
                        LinkCounter = counter,
                    };
                                            
                case Module.Predmet:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Predmetlar",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/Predmet",
                        LinkCounter = counter,
                    };
                case Module.Part:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Bo'limlar",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/Part",
                        LinkCounter = counter,
                    };
              
                case Module.Login:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Login",
                        IconClassName = "fa fa-sign-in",
                        URLPath = "/Admin/Account/Login",
                        LinkCounter = counter,
                    };
                case Module.Register:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Register",
                        IconClassName = "fa fa-user-plus",
                        URLPath = "/Admin/Account/Register",
                        LinkCounter = counter,
                    };
                case Module.About:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "About",
                        IconClassName = "fa fa-users",
                        URLPath = "/Admin/Home/About",
                        LinkCounter = counter,
                    };
                case Module.Contact:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Contact",
                        IconClassName = "fa fa-phone",
                        URLPath = "/Admin/Home/Contact",
                        LinkCounter = counter,
                    };
                case Module.Error:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Error",
                        IconClassName = "fa fa-exclamation-triangle",
                        URLPath = "/Admin/Home/Error",
                        LinkCounter = counter,
                    };
                case Module.SuperAdmin:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "User",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/SuperAdmin",
                        LinkCounter = counter,
                    };
                case Module.Role:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Role",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/Role",
                        LinkCounter = counter,
                    };
                case Module.UserLogs:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "UserLogs",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/UserLogs",
                        LinkCounter = counter,
                    };
                case Module.Result:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Test Natijalari",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/Result",
                        LinkCounter = counter,
                    };
                case Module.Localhost:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Ko'chirish(localhostga)",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/CopyTest",
                        LinkCounter = counter,
                    };
                case Module.Site:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Ko'chirish(Saytga)",
                        IconClassName = "fa fa-link",
                        URLPath = "/Admin/CopyTestFromHost",
                        LinkCounter = counter,
                    };

                default:
                    break;
            }

            return null;
        }
    }
}
