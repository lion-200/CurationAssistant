var viewModel = new CurationDetailsViewModel();

function CurationDetailsViewModel(data) {
    var self = this;
    self.Author = ko.observable();
    self.BlogPost = ko.observable();
    self.ValidationSummary = ko.observable();
    self.BaseUrl = ko.observable("https://steemit.com");

    var mapping = {
        'Author': {
            create: function (options) {
                return new AuthorViewModel(options.data, self);
            }
        },
        'BlogPost': {
            create: function (options) {
                return new BlogPostViewModel(options.data, self);
            }
        },
        'Comments': {
            create: function (options) {
                return new ActionViewModel(options.data, self);
            }
        },
        'Posts': {
            create: function (options) {
                return new ActionViewModel(options.data, self);
            }
        },
        'Votes': {
            create: function (options) {
                return new ActionViewModel(options.data, self);
            }
        }
    }

    if (data != null)
        ko.mapping.fromJS(data, mapping, self);

    self.refreshData = function (data) {
        if (data != null)
            ko.mapping.fromJS(data, mapping, self);
    }
}

function ActionViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    if (data != null)
        ko.mapping.fromJS(data, null, self);

    self.createDate = ko.computed(function () {
        return getJsDate(self.TimeStamp());
    });

    self.postLinkComputed = ko.computed(function () {
        var postLink = "";
        if (self.Type() != "vote") {
            postLink = parent.BaseUrl() + '/' + self.Details.parent_permlink() + '/' + '@' + self.Details.author() + '/' + self.Details.permlink();
        }

        return postLink;
    });

    self.voteComputed = ko.computed(function () {
        var votecmp = "";
        if (self.Type() == "vote") {
            votecmp = Math.round(parseFloat(self.Details.weight() / 100)) + "%";
        }

        return votecmp;
    });

    self.commentLinkComputed = ko.computed(function () {
        var prm = "";
        if (self.Type() == "comment") {
            prm = parent.BaseUrl() + '/' + '@' + self.Details.author() + '/' + self.Details.permlink();
        }

        return prm;
    })

    self.voteLinkComputed = ko.computed(function () {
        var voteLink = "";
        if (self.Type() == "vote") {
            voteLink = parent.BaseUrl() + '/' + '@' + self.Details.author() + '/' + self.Details.permlink();
        }

        return voteLink;
    });
}

function AuthorViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    if (data != null)
        ko.mapping.fromJS(data, null, self);

    self.createDate = ko.computed(function () {
        return getJsDate(self.Details.created());
    });
}

function getJsDate(dotNetDate) {
    var jsDate = new Date(parseInt(dotNetDate.substr(6)));

    var minutes = (jsDate.getMinutes() < 10 ? '0' : '') + jsDate.getMinutes();
    var hours = (jsDate.getHours() < 10 ? '0' : '') + jsDate.getHours();
    var jsDateDisplay = jsDate.getDate() + "-" + (jsDate.getMonth() + 1) + "-" + jsDate.getFullYear() + " " + hours + ":" + minutes;

    return jsDateDisplay;
}

function BlogPostViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    if (data != null)
        ko.mapping.fromJS(data, null, self);

    //self.createDate = ko.computed(function () {
    //    return getJsDate(self.Details.created());
    //});

    //self.postLinkComputed = ko.computed(function () {
    //    var postLink = parent.BaseUrl() + '/' + self.Details.parent_permlink() + '/' + '@' + self.Details.author() + '/' + self.Details.permlink();

    //    return postLink;
    //});
}

$("#validateForm").submit(function (e) {
    e.preventDefault(); // avoid to execute the actual submit of the form.
    $("body").loading();

    var form = $(this);
    var url = form.attr('action');
    console.log(url);

    $.ajax({
        type: "POST",
        url: url,
        data: form.serialize(), // serializes the form's elements.
        success: function (result) {
            $("body").loading('stop');
            console.log(result);
            //viewModel = new CureDetailsViewModel(result);
            if ($("#initialized").val() == 1) {
                viewModel.refreshData(result);
            } else {
                viewModel = new CurationDetailsViewModel(result);
                ko.applyBindings(viewModel);
            }

            $("#initialized").val(1);
        },
        error: function (response, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
});