$(function () {
    window.Company = Backbone.Model.extend({
            idAttribute : 'Id'
        });

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

    window.CompanyDetails = Backbone.View.extend({
        render: function () {
            var template = _.template($('#tpl-company-details').html());
            $(this.el).html(template(this.model.toJSON()));
            return this;
        }
    });


    // Router
    var AppRouter = Backbone.Router.extend({

        routes: {
            "": "CompanysList",
            "Companys/:id": "CompanyDetails"
        },

        CompanysList: function () {
            this.companies = new Companies();

            var view = new CompaniesList({ model: this.companies });

            this.companies.fetch();

            $('#content').html(view.render().el);
        },
        CompanyDetails: function (id) {
            var company = this.companies.get(id);
            var view = new CompanyDetails({ model: company });
            $('#content').html(view.render().el);
        }
    });


    var app = new AppRouter();
    Backbone.history.start();
});