
window.Company = Backbone.Model.extend();

window.Companies = Backbone.Collection.extend({
    model: Company,
    url: "/api/Companies"
});



window.CompaniesList = Backbone.View.extend({
    initialize: function () {
        this.model.bind("reset", this.render, this);
    },
    render: function () {
        var template = _.template($('#tpl-company-item').html());
        var container = $(this.el);
        this.model.each(function (company) {
            var html = template(company.toJSON());
            container.append(html);
        });
        return this;
    }
});


// Router
var AppRouter = Backbone.Router.extend({

    routes: {
        "": "CompanysList",
        "wines/:id": "CompanyDetails"
    },

    CompanysList: function () {
        var companies = new Companies();

        var view = new CompaniesList({ model: companies, el: $('#content') });

        companies.fetch();
    },
    CompanyDetails: function (id) {

    }
});

$(function() {
    var app = new AppRouter();
    Backbone.history.start();
});