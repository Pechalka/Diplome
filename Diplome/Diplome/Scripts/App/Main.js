
window.Test = Backbone.View.extend({
    render: function () {
        $(this.el).append('hello wold');
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
        var view = new Test({ el: $('#content') });
        view.render();
    },
    CompanyDetails: function (id) {

    }
});

$(function() {
    var app = new AppRouter();
    Backbone.history.start();
});