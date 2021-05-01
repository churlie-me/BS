export class CartService
{
    items = []
    total
    constructor()
    {
        this.getItems()
    }

    getItems()
    {
        let json = localStorage.getItem('cart')
        if(json)
            this.items = JSON.parse(json)
    }

    addItem(item, qty)
    {
        this.getItems()
        localStorage.removeItem('cart')
        let orderItem = this.items.find(x => x.item.id == item.id)
        if(orderItem)
          orderItem.quantity += qty
        else
          this.items.push({ item : item, name : item.name, quantity : qty, price : item.saleItem.price, articleId: item.id })

        localStorage.setItem('cart', JSON.stringify(this.items))
    }

    removeItem(itemId)
    {
        this.getCartItems()
        localStorage.removeItem('cart')
        let item = this.items.find(i => i.item.id == itemId)
        if(item)
            this.items.splice(this.items.indexOf(item), 1)

        localStorage.setItem('cart', JSON.stringify(this.items))
    }

    getTotal()
    {
        debugger
        this.total = 0
        this.items.forEach(orderItem => {
            this.total += (orderItem.price * orderItem.quantity)
        });
        return this.total
    }
}
