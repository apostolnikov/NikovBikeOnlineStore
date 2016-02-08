using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartModel
/// </summary>
public class CartModel
{
    public string InsertCart(Cart cart)
    {
        try
        {
            NikovBikeShopDBEntities db = new NikovBikeShopDBEntities();
            db.Carts.Add(cart);
            db.SaveChanges();

            return cart.DateParchased + " was succesfully inserted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string UpdateCart(int id, Cart cart)
    {
        try
        {
            NikovBikeShopDBEntities db = new NikovBikeShopDBEntities();

            //Fetch object from db
            Cart p = db.Carts.Find(id);

            p.DateParchased= cart.DateParchased;
            p.ClientId = cart.ClientId;
            p.Amount = cart.Amount;
            p.IsInCart = cart.IsInCart;
            p.ProductId = cart.ProductId;

            db.SaveChanges();
            return cart.DateParchased + " was succesfully updated";

        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string DeleteCart(int id)
    { 
        try
        {
            NikovBikeShopDBEntities db = new NikovBikeShopDBEntities();
            Cart cart = db.Carts.Find(id);

            db.Carts.Attach(cart);
            db.Carts.Remove(cart);
            db.SaveChanges();

            return cart.DateParchased + " was succesfully deleted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }
}