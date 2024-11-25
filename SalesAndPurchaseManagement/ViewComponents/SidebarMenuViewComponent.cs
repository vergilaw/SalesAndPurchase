﻿using Microsoft.AspNetCore.Mvc;
using SalesAndPurchaseManagement.Models;
using System.Security.Claims;

public class SidebarMenuViewComponent : ViewComponent
{
    private List<MenuItem> MenuItems = new List<MenuItem>();

    public SidebarMenuViewComponent()
    {
        MenuItems = new List<MenuItem>()
        {
            new MenuItem() { Id = 1, Name = "Tổng quan", Link = "/Dashboard/Index", Icon = "fas fa-tachometer-alt", IsAdmin = false },
            new MenuItem() { Id = 2, Name = "Hóa đơn nhập hàng", Link = "/PurchaseInvoice/Index", Icon = "fas fa-file-invoice", IsAdmin = true },
            new MenuItem() { Id = 3, Name = "Hóa đơn bán hàng", Link = "/SalesInvoice/Index", Icon = "fas fa-file-invoice-dollar", IsAdmin = true },
            new MenuItem() { Id = 4, Name = "Nhân viên", Link = "/Employee/Index", Icon = "fas fa-user", IsAdmin = true },
            new MenuItem() { Id = 5, Name = "Vị trí công việc", Link = "/Job/Index", Icon = "fas fa-briefcase", IsAdmin = true },
            new MenuItem()
            {
                Id = 6,
                Name = "Sản phẩm",
                Link = "#",
                Icon = "fas fa-box",
                IsAdmin = false,
                SubItems = new List<MenuItem>
                {
                    new MenuItem() { Id = 7, Name = "Chi tiết sản phẩm", Link = "/Product/Index", IsAdmin = false },
                    new MenuItem() { Id = 8, Name = "Danh mục", Link = "/Category/Index", IsAdmin = false },
                    new MenuItem() { Id = 9, Name = "Xuất xứ", Link = "/CountryOfOrigin/Index", IsAdmin = false },
                    new MenuItem() { Id = 10, Name = "Đặc điểm", Link = "/Characteristic/Index", IsAdmin = false },
                    new MenuItem() { Id = 11, Name = "Vật liệu", Link = "/Material/Index", IsAdmin = false },
                    new MenuItem() { Id = 12, Name = "Tính năng", Link = "/Feature/Index", IsAdmin = false },
                    new MenuItem() { Id = 13, Name = "Hình dạng", Link = "/Shape/Index", IsAdmin = false },
                    new MenuItem() { Id = 14, Name = "Nhà sản xuất", Link = "/Manufacturer/Index", IsAdmin = false }
                }
            },
            new MenuItem() { Id = 15, Name = "Khách hàng", Link = "/Customer/Index", Icon = "fas fa-users", IsAdmin = false },
            new MenuItem() { Id = 16, Name = "Nhà cung cấp", Link = "/Supplier/Index", Icon = "fas fa-truck", IsAdmin = false },
            new MenuItem() { Id = 17, Name = "Thống kê doanh thu", Link = "/Dashboard/Statistics", Icon = "fas fa-chart-line", IsAdmin = false },
            new MenuItem() { Id = 18, Name = "Đăng xuất", Link = "/Home/Logout", Icon = "fas fa-sign-out-alt", IsAdmin = false }
        };
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var isAdmin = (User as ClaimsPrincipal)?.Claims.Any(c => c.Type == "IsAdmin" && c.Value == "True") ?? false;
        var filteredMenu = MenuItems
            .Where(item => !item.IsAdmin || isAdmin)
            .Select(item =>
            {
                item.SubItems = item.SubItems.Where(subItem => !subItem.IsAdmin || isAdmin).ToList();
                return item;
            }).ToList();

        return View("SidebarMenu", filteredMenu);
    }
}
