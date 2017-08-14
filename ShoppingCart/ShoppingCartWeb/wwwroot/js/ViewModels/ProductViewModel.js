var productViewModel;

function Product(id, categoryId, categoryName, name, price) {
    var self = this;

    self.Id = ko.observable(id);
    self.CategoryId = ko.observable(categoryId).extend({ required: "Category is required." });
    self.CategoryName = ko.observable(categoryName);
    self.Name = ko.observable(name).extend({ required: "Name is required." });
    self.Price = ko.observable(price).extend({ required: "Price is required." });

    self.categories = ko.observableArray([]);
    self.categories.push('');

    $.getJSON('http://localhost:49376/api/Category/GetCategories/', function (data) {
        $.each(data, function (key, value) {
            self.categories.push(value);
        });
    });

    self.saveProduct = function () {
        var dataObject = ko.toJSON(this);

        $.ajax({
            url: 'http://localhost:49376/api/Products/SaveProduct',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                productViewModel.productListViewModel.getProducts();

                self.Id(null);
                self.CategoryId(null);
                self.Name('');
                self.Price(null);
            }
        });
    };
}

function ProductList() {
    var self = this;
    
    self.products = ko.observableArray([]);
    
    self.getProducts = function () {
        self.products.removeAll();
        $.getJSON('http://localhost:49376/api/Product/GetProducts', function (data) {
            $.each(data, function (key, value) {
                self.products.push(new Product(value.id, value.categoryId, value.category.name, value.name, value.price));
            });
        });
    };

    self.editProduct = function (product) {
        $.getJSON('http://localhost:49376/api/Products/' + product.Id(), function (data) {
            productViewModel.addProductViewModel.Id(data.id);
            productViewModel.addProductViewModel.CategoryId(data.categoryId);
            productViewModel.addProductViewModel.Name(data.name);
            productViewModel.addProductViewModel.Price(data.price);
        });
    };
}

productViewModel = { addProductViewModel: new Product(), productListViewModel: new ProductList() };

$(document).ready(function () {
    ko.applyBindings(productViewModel);
    productViewModel.productListViewModel.getProducts();
});