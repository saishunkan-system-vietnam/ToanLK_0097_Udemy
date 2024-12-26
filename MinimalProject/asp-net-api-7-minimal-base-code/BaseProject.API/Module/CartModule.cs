using Amazon.S3;
using Amazon.S3.Model;
using BaseProject.API.Auth;
using BaseProject.API.ServiceHelper;
using BaseProject.Core.Mail;
using BaseProject.Core.Service;
using BaseProject.Core.Token;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;
using BaseProject.Shared.Modal.Enum;
using BaseProject.Shared.ResponseObject;
using GrapeCity.Enterprise.Data.Expressions.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace BaseProject.API.Module
{
    public class CartModule : IModule
    {

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var cart = endpoints.MapGroup("/cart")
                        .WithTags("cart");


            cart.MapGet("/", GetProductByAccount);
            cart.MapGet("/{productId}/{quantity}", InsertProductToCart);
            cart.MapPut("/{cartId}/{increaseStep}", UpdateMountCart);
            cart.MapPut("/{cartId}/{status}/{delFlag}", UpdateStatus);
            cart.MapGet("/get-in-order", GetByInOrder);
            cart.MapGet("/get-finished", GetByFinished);
            cart.MapDelete("/{cartId}",DeleteByCartId);
            cart.MapPut("/selected/{cartId}", ChangeSelectedById);



            return endpoints;
        }

        public R<bool> InsertProductToCart(Guid productId,int quantity, HttpContext context, [FromServices] ICartService cartService, [FromServices] IAccountService accountService, [FromServices] TokenService  tokenService)
        {
            var currentUser = accountService.GetMe(context);
            if (currentUser == null)
            {
                return new R<bool>()
                {
                    Payload = false
                };
            }; 
           var insertProductResult =  cartService.InsertIntoCart((Guid)currentUser.Id, productId, quantity);
            return new R<bool>()
            {
                Payload = insertProductResult
            };
        }

        public R<List<Cart>?> GetProductByAccount(HttpContext context, [FromServices] IAccountService accountService, [FromServices] ICartService cartService)
        {
            var currentAccount = accountService.GetMe(context);
            if(currentAccount == null)
            {
                return new R<List<Cart>>()
                {
                    Payload = null
                };
            }
            var currentAccountId = currentAccount.Id;
            var cartList = cartService.GetProductByCart((Guid)currentAccountId);
            return new R<List<Cart>?>()
            {
                Payload = cartList
            };
        }

        public R<List<Cart>> GetByInOrder(HttpContext context,[FromServices]IAccountService accountService, [FromServices] ICartService cartService)
        {
            var result = cartService.GetByWhere(c => c.Status == "InOrder" && c.DelFlag == true).Include(c=>c.Product).Include(c => c.Account).OrderBy(c=>c.AccountId).ToList();
            return new R<List<Cart>>()
            {
                Payload = result
            };
        }

        public R<List<Cart>> GetByFinished(HttpContext context, [FromServices] IAccountService accountService, [FromServices] ICartService cartService)
        {
            var result = cartService.GetByWhere(c => c.Status == "Finished" && c.DelFlag == true).Include(c => c.Product).Include(c => c.Account).OrderBy(c => c.AccountId).ToList();
            return new R<List<Cart>>()
            {
                Payload = result
            };
        }

        public R<bool> UpdateMountCart(Guid cartId, int increaseStep, [FromServices] ICartService cartService, [FromServices] IProductService productService)
        {
            var currentCart = cartService.GetByWhere(p => p.Id == cartId).FirstOrDefault();
            var currentProduct = productService.GetByWhere(p => p.Id == currentCart.ProductId).FirstOrDefault();
            if(currentCart.Mount != 0 && increaseStep < 0)
            {
                currentCart.Mount += increaseStep;
            }
            else if(currentCart.Mount == 0 && increaseStep < 0)
            {
                return new R<bool>()
                {
                    Payload = false,
                    Code = 500,
                    Message = "You could not decrease your product"
                };
            }
            if (currentCart.Mount < currentProduct.Mounts && increaseStep > 0)
            {
                currentCart.Mount += increaseStep;
            }
            else if(currentCart.Mount > currentProduct.Mounts && increaseStep < 0)
            {
                return new R<bool>()
                {
                    Payload = false,
                    Code = 500,
                    Message = "There are not enough products in our container"
                };
            }
            var result = cartService.Update(currentCart);
            return new R<bool>()
            {
                Payload = result
            };
        }

        public R<bool> UpdateStatus(Guid cartId,string status, bool delFlag, [FromServices] ICartService cartService)
        {
            var currentCart = cartService.GetByWhere(p => p.Id == cartId).FirstOrDefault();
            currentCart.Status = status;
            currentCart.DelFlag = delFlag;
            var result = cartService.Update(currentCart);
            return new R<bool>()
            {
                Payload = result
            };
        }

        public R<bool> DeleteByCartId(Guid cartId, [FromServices] ICartService cartService)
        {
            var result = cartService.DeleteCartById(cartId);
            return new R<bool>()
            {
                Payload = result
            };
        }
        
        public R<bool> ChangeSelectedById(Guid cartId, [FromServices] ICartService cartService)
        {
            var result = cartService.ChangeSelectedById(cartId);
            return new R<bool>()
            {
                Payload = result
            };
        }
    }
}
