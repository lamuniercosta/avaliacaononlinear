function formatCurrency(value) {
    return "$" + value.toFixed(2);
}

var Product = function (id, name, price) {
    var self = this;

    self.id = ko.observable(id);
    self.name = ko.observable(name);
    self.price = ko.observable(price);
};

var CartItem = function (id, cartId, product, quantity) {
    var self = this;

    self.id = ko.observable(id);
    self.cartId = ko.observable(cartId);
    self.product = ko.observable(product);
    self.productId = self.product().id();
    self.quantity = ko.observable(quantity || 1);
    self.cost = ko.computed(function () {
        return self.product().price() * self.quantity();
    });
    self.editing = ko.observable(false);
    self.edit = function () { self.editing(true) };    
};

var Cart = function (id, cartItems) {
    var self = this;

    self.id = ko.observable(id);
    self.cartItems = ko.observable(cartItems);
}

var ViewModel = function () {
    var self = this; 

    self.id = ko.observable();
    self.cart = ko.observableArray();
    self.products = ko.observableArray([]);
    self.selectedProduct = ko.observable();
    self.products.push('');

    self.getCart = function (id, event) {
        $.getJSON('http://localhost:49376/api/Carts/' + id, function (data) {
            $.each(data, function (key, value) {
                var product = new Product(value.product.id, value.product.name, value.product.price)
                cart_item = new CartItem(value.id, id, product, value.quantity);
                self.cart.push(cart_item);
                self.id(id);
            });
        });
    }

    $.getJSON('http://localhost:49376/api/Product/GetProducts/', function (data) {
        $.each(data, function (key, value) {
            self.products.push(value);
        });
    }); 

    self.subtotal = ko.computed(function () {
        var subtotal = 0;
        $(self.cart()).each(function (index, cart_item) {
            subtotal += cart_item.cost();
        });
        return subtotal;
    });

    self.total = ko.computed(function () {
        return self.subtotal();
    });

    self.addToCart = function () {
        var cart_item;
        $.getJSON('http://localhost:49376/api/Products/' + this.selectedProduct(), function (data) {
            var product = new Product(data.id, data.name, data.price);
            cart_item = new CartItem(0, 0, product, 1);
            var match = ko.utils.arrayFirst(self.cart(), function (item) {
                return cart_item.product().name() === item.product().name();
            });
            if (match) {
                match.quantity(match.quantity() + 1);
            }
            else {
                self.cart.push(cart_item);
            }
        });
        self.selectedProduct('');
    };

    self.removeFromCart = function (cart_item, event) {
        self.cart.remove(cart_item);
    };

    self.closeOrder = function () {
        var cartObj = new Cart(self.id(),self.cart());
        var dataObject = ko.toJSON(cartObj);

        $.ajax({
            url: 'http://localhost:49376/api/Carts/SaveCart',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                window.location.href = '/Sale/' + data.id;
            }
        });
    }
};

window.view_model = new ViewModel();

$(document).ready(function () {

    var paramsString = new String(window.location);
    var id = paramsString.substring(paramsString.lastIndexOf('\/') + 1, paramsString.length);
    if (id > 0) {
        view_model.getCart(id);
    }
    ko.applyBindings(window.view_model);

});