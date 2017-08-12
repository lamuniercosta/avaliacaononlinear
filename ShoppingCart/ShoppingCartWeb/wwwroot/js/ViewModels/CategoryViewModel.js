var categoryViewModel;

// use as register category views view model
function Category(id, name) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(id);
    self.Name = ko.observable(name).extend({ required: "Name is required." });

    self.saveCategory = function () {
        var dataObject = ko.toJSON(this);

        $.ajax({
            url: 'http://localhost:49376/api/Categories/SaveCategory',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                categoryViewModel.categoryListViewModel.getCategories();

                self.Id(null);
                self.Name('');
            }
        });
    };
}

// use as category list view's view model
function CategoryList() {

    var self = this;

    // observable arrays are update binding elements upon array changes
    self.categories = ko.observableArray([]);

    self.getCategories = function () {
        self.categories.removeAll();

        // retrieve categories list from server side and push each object to model's categories list
        $.getJSON('http://localhost:49376/api/Category/GetCategories', function (data) {
            $.each(data, function (key, value) {
                self.categories.push(new Category(value.id, value.name));
            });
        });
    };

    self.editCategory = function (category) {

        // retrieve categories list from server side and push each object to model's categories list
        $.getJSON('http://localhost:49376/api/Categories/' + category.Id(), function (data) {
            categoryViewModel.addCategoryViewModel.Id(data.id);
            categoryViewModel.addCategoryViewModel.Name(data.name);
        });
    };


    // remove category. current data context object is passed to function automatically.
    //self.removeCategory = function (category) {
    //    $.ajax({
    //        url: 'http://localhost:49376/api/Categories/DeleteCategory' + category.Id(),
    //        type: 'delete',
    //        contentType: 'application/json',
    //        success: function () {
    //            self.categories.remove(category);
    //        }
    //    });
    //};
}


// create index view view model which contain two models for partial views
categoryViewModel = { addCategoryViewModel: new Category(), categoryListViewModel: new CategoryList() };


// on document ready
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(categoryViewModel);

    // load category data
    categoryViewModel.categoryListViewModel.getCategories();
});