var paymentViewModel;

// use as register payment views view model
function Payment(id, name) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(id);
    self.Name = ko.observable(name).extend({ required: "Name is required." });

    self.savePayment = function () {
        var dataObject = ko.toJSON(this);

        $.ajax({
            url: 'http://localhost:49376/api/Payments/SavePayment',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                paymentViewModel.paymentListViewModel.getPayments();

                self.Id(null);
                self.Name('');
            }
        });
    };
}

// use as payment list view's view model
function PaymentList() {

    var self = this;

    // observable arrays are update binding elements upon array changes
    self.payments = ko.observableArray([]);

    self.getPayments = function () {
        self.payments.removeAll();

        // retrieve payments list from server side and push each object to model's payments list
        $.getJSON('http://localhost:49376/api/Payment/GetPayments', function (data) {
            $.each(data, function (key, value) {
                self.payments.push(new Payment(value.id, value.name));
            });
        });
    };

    self.editPayment = function (payment) {

        // retrieve payments list from server side and push each object to model's payments list
        $.getJSON('http://localhost:49376/api/Payments/' + payment.Id(), function (data) {
            paymentViewModel.addPaymentViewModel.Id(data.id);
            paymentViewModel.addPaymentViewModel.Name(data.name);
        });
    };


    // remove payment. current data context object is passed to function automatically.
    //self.removePayment = function (payment) {
    //    $.ajax({
    //        url: 'http://localhost:49376/api/Payments/DeletePayment' + payment.Id(),
    //        type: 'delete',
    //        contentType: 'application/json',
    //        success: function () {
    //            self.payments.remove(payment);
    //        }
    //    });
    //};
}


// create index view view model which contain two models for partial views
paymentViewModel = { addPaymentViewModel: new Payment(), paymentListViewModel: new PaymentList() };


// on document ready
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(paymentViewModel);

    // load payment data
    paymentViewModel.paymentListViewModel.getPayments();
});