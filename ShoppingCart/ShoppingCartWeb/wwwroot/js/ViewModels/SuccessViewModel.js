function formatCurrency(value) {
    return "$" + value.toFixed(2);
}

var ViewModel = function () {
    var self = this;
    self.Id = ko.observable();
    self.DtSale = ko.observable();
    self.Payment = ko.observable();
    self.Total = ko.observable();

    self.getSale = function (id, event) {
        $.getJSON('http://localhost:49376/api/Sales/' + id, function (data) {
            self.Id(data.id);
            self.DtSale(moment(data.dtSale).format('L'));
            self.Payment(data.payment.name);
            self.Total(formatCurrency(data.total));
        });
    }
}

window.view_model = new ViewModel();

$(document).ready(function () {

    var paramsString = new String(window.location);
    var id = paramsString.substring(paramsString.lastIndexOf('\/') + 1, paramsString.length);
    if (id > 0) {
        view_model.getSale(id);
    }
    ko.applyBindings(window.view_model);

});