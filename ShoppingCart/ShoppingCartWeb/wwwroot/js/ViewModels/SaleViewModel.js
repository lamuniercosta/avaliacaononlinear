function formatCurrency(value) {
    return "$" + value.toFixed(2);
}

var Sale = function (cartId, paymentId, total) {
    var self = this;

    self.CartId = cartId;
    self.PaymentId = paymentId;
    self.Total = total;
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
};


var ViewModel = function () {
    var self = this;

    self.id = ko.observable();
    self.cart = ko.observableArray();
    self.payments = ko.observableArray([]);
    self.selectedPayment = ko.observable();
    self.payments.push('');

    self.total = ko.computed(function () {
        var subtotal = 0;
        $(self.cart()).each(function (index, cart_item) {
            subtotal += cart_item.cost();
        });
        return subtotal;
    });

    self.reviewCart = function () {
        window.location.href = '/Cart/' + self.id();
    }

    $.getJSON('http://localhost:49376/api/Payment/GetPayments/', function (data) {
        $.each(data, function (key, value) {
            self.payments.push(value);
        });
    });

    self.getCart = function (id, event) {
        $.getJSON('http://localhost:49376/api/Sale/GetCartById/' + id, function (data) {
            if (data.saleId != 0) {
                window.location.href = '/Sale/Success/' + data.saleId;
            }
            else {
                $.getJSON('http://localhost:49376/api/Carts/' + id, function (data) {
                    $.each(data, function (key, value) {
                        var product = new Product(value.product.id, value.product.name, value.product.price)
                        cart_item = new CartItem(value.id, id, product, value.quantity);
                        self.cart.push(cart_item);
                    });
                    self.id(id);
                });
            }
        });
    }    

    self.closeOrder = function () {
        var cartObj = new Sale(self.id(), self.selectedPayment(), self.total());
        var dataObject = ko.toJSON(cartObj);

        $.ajax({
            url: 'http://localhost:49376/api/Sales/SaveSale',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                window.location.href = '/Sale/Success/' + data.id;
            }
        });
    }
}

window.view_model = new ViewModel();

$(document).ready(function () {

    var paramsString = new String(window.location);
    var id = paramsString.substring(paramsString.lastIndexOf('\/') + 1, paramsString.length);
    if (id > 0) {
        view_model.getCart(id);
    }
    ko.applyBindings(window.view_model);

});