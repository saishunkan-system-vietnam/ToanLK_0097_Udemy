using AutoMapper;
using BaseProject.Core.Mail;
using BaseProject.Core.Token;
using BaseProject.Infrastructure;
using BaseProject.Shared;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;
using BaseProject.Shared.Modal.Enum;
using BaseProject.Shared.StringHelper;
using GrapeCity.Enterprise.Data.Expressions.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BaseProject.Core.Service
{
    public class CartService : GenericService<Cart>, ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMailSender _mailSender;
        private readonly IMapper _mapper;
        public CartService(IUnitOfWork repository, TokenService tokenService, IMailSender mailSender, IMapper mapper, IConfiguration configuration) : base(repository)
        {
            _unitOfWork = repository;
            _tokenService = tokenService;
            _mailSender = mailSender;
            _mapper =  mapper;
            _configuration = configuration;
        }

        public bool InsertIntoCart(Guid accountId, Guid productId, int quantity)
        {

            var existedProductCart = _unitOfWork.CartRepository.GetByWhere(c => c.AccountId == accountId && c.ProductId == productId && c.DelFlag == false).FirstOrDefault();
            var currentProduct = _unitOfWork.ProductRepository.GetByWhere(x => x.Id == productId).FirstOrDefault();

            if (existedProductCart != null)
            {
                var productAfterAdded = existedProductCart.Mount + quantity;
                if (productAfterAdded <= currentProduct.Mounts)
                {
                        existedProductCart.Mount += quantity;
                    _unitOfWork.CartRepository.Update(existedProductCart);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (quantity <= currentProduct.Mounts)
                {

                    var addedCart = new Cart()
                    {
                        Id = Guid.NewGuid(),
                        AccountId = accountId,
                        ProductId = productId,
                        Mount = quantity,
                        // TODO: change it : InCart, InOrder, Finished
                        Status = "InCart",
                        DelFlag = false
                    };
                    _unitOfWork.CartRepository.Add(addedCart);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            return true;

        }

        public List<Cart> GetProductByCart(Guid accountId)
        {
            var cartList = _unitOfWork.CartRepository.GetByWhere(c => c.AccountId == accountId && !c.DelFlag).Include(c => c.Product).ToList();
            return cartList;
        }

        public bool DeleteCartById(Guid cartId)
        {
            return _unitOfWork.CartRepository.Delete(x => x.Id == cartId);
        }

        public bool ChangeSelectedById(Guid cartId)
        {
            var currentCart = _unitOfWork.CartRepository.GetByWhere(x => x.Id == cartId).FirstOrDefault();
            currentCart.Selected = !currentCart.Selected;
            _unitOfWork.CartRepository.Update(currentCart);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
