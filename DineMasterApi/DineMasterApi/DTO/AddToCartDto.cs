﻿namespace DineMasterApi.DTO
{
    public class AddToCartDto
    {
        public int UserId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
