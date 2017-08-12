var productViewModel;

// use as register product views view model
function Product(id, categoryId, categoryName, name, price) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(id);
    self.CategoryId = ko.observable(categoryId).extend({ required: "Category is required." });
    self.CategoryName = ko.observable(categoryName);
    self.Name = ko.observable(name).extend({ required: "Name is required." });
    self.Price = ko.observable(price).extend({ required: "Price is required." });

    self.categories = ko.observableArray([]);
    self.categories.push('');

    // retrieve products list from server side and push each object to model's products list
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

// use as product list view's view model
function ProductList() {

    var self = this;
    
    // observable arrays are update binding elements upon array changes
    self.products = ko.observableArray([]);
    
    self.getProducts = function () {
        self.products.removeAll();

        // retrieve products list from server side and push each object to model's products list
        $.getJSON('http://localhost:49376/api/Product/GetProducts', function (data) {
            $.each(data, function (key, value) {
                self.products.push(new Product(value.id, value.categoryId, value.category.name, value.name, value.price));
            });
        });
    };

    self.editProduct = function (product) {

        // retrieve products list from server side and push each object to model's products list
        $.getJSON('http://localhost:49376/api/Products/' + product.Id(), function (data) {
            productViewModel.addProductViewModel.Id(data.id);
            productViewModel.addProductViewModel.CategoryId(data.categoryId);
            productViewModel.addProductViewModel.Name(data.name);
            productViewModel.addProductViewModel.Price(data.price);
        });
    };


    // remove product. current data context object is passed to function automatically.
    //self.removeProduct = function (product) {
    //    $.ajax({
    //        url: 'http://localhost:49376/api/Products/DeleteProduct' + product.Id(),
    //        type: 'delete',
    //        contentType: 'application/json',
    //        success: function () {
    //            self.products.remove(product);
    //        }
    //    });
    //};
}


// create index view view model which contain two models for partial views
productViewModel = { addProductViewModel: new Product(), productListViewModel: new ProductList() };


// on document ready
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(productViewModel);

    // load product data
    productViewModel.productListViewModel.getProducts();
});