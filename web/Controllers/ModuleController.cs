using CommunAxiom.Transformations.AppModel.Business;
using CommunAxiom.Transformations.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using web.Helpers;

namespace web.Controllers
{
    public class ModuleController : Controller
    {
        private readonly IModuleBusiness moduleBusiness;

        public ModuleController(IModuleBusiness moduleBusiness)
        {
            this.moduleBusiness = moduleBusiness;
        }

        // GET: ModuleController
        public ActionResult Index(string search="")
        {
            var res = this.moduleBusiness.ListModule(search);
            return View(res);
        }

        // GET: ModuleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await this.HandleResult(OperationType.READ, async () =>
            {
                var mod = await this.moduleBusiness.GetModule(id);
                if (mod == null)
                {
                    return NotFound();
                }
                return View(mod);
            }, x =>
            {
                this.SetErrors<Module>(x);
                return View();
            });
        }

        // GET: ModuleController/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: ModuleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Module module)
        {
            return await this.HandleResult(OperationType.CREATE, async () =>
            {
                var res = await this.moduleBusiness.AddModule(module);
                if (!res.ValidationResult.IsValid)
                {
                    this.SetErrors(res);
                    return View("Create");
                }
                return RedirectToAction(nameof(Index));
            }, x =>
            {
                this.SetErrors<Module>(x);
                return View("Create");
            });
                        
        }

        // GET: ModuleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await this.HandleResult(OperationType.READ, async () =>
            {
                var res = await this.moduleBusiness.GetModule(id);
                if (res == null)
                {
                    return NotFound();
                }
                ViewData["different"] = false;
                return View("Edit", res);
            }, x =>
            {
                this.SetErrors<Module>(x);
                return View("Create");
            });
        }

        // POST: ModuleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Module module)
        {
            return await this.HandleResult(OperationType.UPDATE, async () =>
            {
                var res = await this.moduleBusiness.UpdateModule(module, false);
                if (!res.ValidationResult.IsValid)
                {
                    if(res.ValidationResult.Errors.Count == 1 && res.ValidationResult.Errors[0].ErrorCode == ERROR_CODES.OVERWRITE)
                    {
                        ViewData["different"] = true;
                        return View("Edit");
                    }
                    this.SetErrors(res);
                    ViewData["different"] = false;
                    return View("Edit");
                }
                return RedirectToAction(nameof(Index));
            }, x =>
            {
                this.SetErrors<Module>(x);
                ViewData["different"] = false;
                return View("Edit");
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Overwrite(Module module, bool overwrite)
        {
            if (overwrite)
            {
                return await this.HandleResult(OperationType.UPDATE, async () =>
                {
                    var res = await this.moduleBusiness.UpdateModule(module, true);
                    if (!res.ValidationResult.IsValid)
                    {
                        this.SetErrors(res);
                        return View("Edit");
                    }
                    return RedirectToAction(nameof(Index));
                }, x =>
                {
                    this.SetErrors<Module>(x);
                    return View("Edit");
                });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ModuleController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return await this.HandleResult(OperationType.READ, async () =>
            {
                var res = await this.moduleBusiness.GetModule(id);
                if (res == null)
                {
                    return NotFound();
                }
                return View("Delete", res);
            }, x =>
            {
                this.SetErrors<Module>(x);
                return View("Delete");
            });
        }

        // POST: ModuleController/Delete/5
        [Authorize(Policy = "RequireAdministrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            return await this.HandleResult(OperationType.UPDATE, async () =>
            {
                var res = await this.moduleBusiness.DeleteModule(id);
                if (!res.IsValid)
                {
                    this.SetErrors(res);
                    return View("Delete");
                }
                return RedirectToAction(nameof(Index));
            }, x =>
            {
                this.SetErrors<Module>(x);
                return View("Delete");
            });
        }
    }
}
