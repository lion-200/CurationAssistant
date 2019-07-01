
ko.bindingHandlers.tooltip = {
    init: function (element, valueAccessor) {
        var local = ko.utils.unwrapObservable(valueAccessor()),
            options = {};

        ko.utils.extend(options, ko.bindingHandlers.tooltip.options);
        ko.utils.extend(options, local);

        $(element).tooltip(options);

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).tooltip("destroy");
        });
    },
    options: {
        placement: "right",
        trigger: "click"
    }
};

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
                return new DiscussionListViewModel(options.data, self);
            }
        },
        'Votes': {
            create: function (options) {
                return new ActionViewModel(options.data, self);
            }
        },
        'UpvoteAccountVotes': {
            create: function (options) {
                return new GetAccountVotesViewModel(options.data, self);
            }
        },
        'ValidationSummary': {
            create: function (options) {
                return new ValidationSummaryViewModel(options.data, self);
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

function ValidationSummaryViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    var mapping = {
        'Items': {
            create: function (options) {
                return new ValidationItemViewModel(options.data, self);
            }
        }
    }

    if (data != null)
        ko.mapping.fromJS(data, mapping, self);
}

function ValidationItemViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    if (data != null)
        ko.mapping.fromJS(data, null, self);

    self.ResultDisplayComputed = ko.computed(function () {
        //return self.ResultTypeDescription() + " - " + self.ResultMessage();
        return self.ResultMessage();
    })

    self.ClassComputed = ko.computed(function () {
        var divClass = "alert alert-";

        if (self.ResultTypeDescription() == "Success") {
            divClass = divClass + "success";
        } else if (self.ResultTypeDescription() == "Failure") {
            divClass = divClass + "danger";
        } else {
            divClass = divClass + "warning";
        }

        divClass += " card-header";

        return divClass;
    });
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
    var offset = new Date().getTimezoneOffset();
    var jsDate = new Date(parseInt(dotNetDate.substr(6)));     
    jsDate.setMinutes(jsDate.getMinutes() - offset);    

    var minutes = ("0" + jsDate.getMinutes()).slice(-2);//(jsDate.getMinutes() < 10 ? '0' : '') + jsDate.getMinutes();
    var hours = ("0" + jsDate.getHours()).slice(-2);//(jsDate.getHours() < 10 ? '0' : '') + jsDate.getHours();
    var getDate = ("0" + jsDate.getDate()).slice(-2);
    var jsDateDisplay = getDate + "-" + (jsDate.getMonth() + 1) + "-" + jsDate.getFullYear() + " " + hours + ":" + minutes;

    //if (showDateDiff) {
    //    var jsDateUnix = new Date(jsDate.getFullYear(), jsDate.getMonth(), jsDate.getDate(), jsDate.getHours(), jsDate.getMinutes(), jsDate.getSeconds());
    //    jsDateDisplay += "<br /> (" + getJsDateDiff(jsDateUnix) + ")";
    //}

    return jsDateDisplay;
}

function getJsDateDiff(dotNetDate) {
    var offset = new Date().getTimezoneOffset();
    var jsDate = new Date(parseInt(dotNetDate.substr(6)));
    jsDate.setMinutes(jsDate.getMinutes() - offset);    

    var delta = Math.abs(Date.now() - jsDate) / 1000;

    // calculate (and subtract) whole days
    var days = Math.floor(delta / 86400);
    delta -= days * 86400;

    // calculate (and subtract) whole hours
    var hours = Math.floor(delta / 3600) % 24;
    delta -= hours * 3600;

    // calculate (and subtract) whole minutes
    var minutes = Math.floor(delta / 60) % 60;
    delta -= minutes * 60;

    // what's left is seconds
    //var seconds = delta % 60;  // in theory the modulus is not required

    return "(" + days + " days " + hours + " hours " + minutes + " minutes ago)";
}

function BlogPostViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    if (data != null)
        ko.mapping.fromJS(data, null, self);

    self.createDate = ko.computed(function () {
        return getJsDate(self.Details.created());
    });    

    self.postLinkComputed = ko.computed(function () {
        var postLink = parent.BaseUrl() + '/' + self.Details.parent_permlink() + '/' + '@' + self.Details.author() + '/' + self.Details.permlink();

        return postLink;
    });
}

function DiscussionListViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    if (data != null)
        ko.mapping.fromJS(data, null, self);

    self.createDate = ko.computed(function () {
        return getJsDate(self.CreatedAt());
    });

    self.createDateDiff = ko.computed(function () {
        return getJsDateDiff(self.CreatedAt());
    });

    self.postLinkComputed = ko.computed(function () {
        var postLink = parent.BaseUrl() + '/' + self.ParentPermlink() + '/' + '@' + self.Author() + '/' + self.Permlink();

        return postLink;
    });

    self.pendingPayoutComputed = ko.computed(function () {
        var displayValue = '$' + self.PendingPayout()
        if (self.IsResteem())
            displayValue = "";

        return displayValue;
    });

    self.authorPayoutComputed = ko.computed(function () {
        var displayValue = '$' + self.PaidOut()
        if (self.IsResteem())
            displayValue = "";

        return displayValue;
    });

    self.totalPayoutComputed = ko.computed(function () {
        var displayValue = '$' + self.PaidOutTotal()
        if (self.IsResteem())
            displayValue = "";

        return displayValue;
    });
}

function GetAccountVotesViewModel(data, parent) {
    var self = this;
    self.parent = parent;
    self.LastVotes = ko.observableArray();

    var mapping = {
        'LastVotes': {
            create: function (options) {
                return new FindVotesItemViewModel(options.data, self);
            }
        }
    };

    if (data != null)
        ko.mapping.fromJS(data, mapping, self);
}


function FindVotesItemViewModel(data, parent) {
    var self = this;
    self.parent = parent;

    if (data != null)
        ko.mapping.fromJS(data, null, self);

    self.createDate = ko.computed(function () {
        return getJsDate(self.last_update());
    });

    self.createDateDiff = ko.computed(function () {
        return getJsDateDiff(self.last_update());
    });

    self.postLinkComputed = ko.computed(function () {
        var postLink = parent.parent.BaseUrl() + '/' + '@' + self.author() + '/' + self.permlink();

        return postLink;
    });
}

$("#validateForm").submit(function (e) {
    e.preventDefault(); // avoid to execute the actual submit of the form.
    $("body").loading();

    var form = $(this);
    var url = form.attr('action');    

    $.ajax({
        type: "POST",
        url: url,
        data: form.serialize(), // serializes the form's elements.
        success: function (result) {
            $("body").loading('stop');
            console.log(result);
            if ($("#initialized").val() == 1) {
                viewModel.refreshData(result);
            } else {
                viewModel = new CurationDetailsViewModel(result);
                ko.applyBindings(viewModel);
            }

            $("#initialized").val(1);
            $(".validationContainer").show();
        },
        error: function (response, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
});


$(document).ready(function () {
    $(".validationContainer").hide();

    $('.validationContainer').on('hide.bs.collapse', function (e) {
        var clicked = $(document).find("[href='#" + $(e.target).attr('id') + "']");
        clicked.find('.valIcon').removeClass("fa-chevron-up").addClass("fa-chevron-down");
    })
    $('.validationContainer').on('show.bs.collapse', function (e) {
        var clicked = $(document).find("[href='#" + $(e.target).attr('id') + "']");
        clicked.find('.valIcon').removeClass("fa-chevron-down").addClass("fa-chevron-up");
    })
});