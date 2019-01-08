var weddyModule = { };
(function () {
    weddyModule.Details = function() {

        var createObservableEntry = function(entry) {
            return {
                Id: ko.observable(entry.Id),
                Name: ko.observable(entry.Name),
                Email: ko.observable(entry.Email),
                Amount: ko.observable(entry.Amount),
                DeviationMin: ko.observable(parseInt(entry.DeviationMin)),
                DeviationMax: ko.observable(parseInt(entry.DeviationMax)),
                DeviationText: ko.observable(entry.DeviationText),
                Gravatar: ko.observable(entry.Gravatar)
            };
        };
        var getDetails = function(poolId) {
            $.getJSON(
                "/api/weddyApi/" + poolId,
                null,
                function(data) {
                    viewModel.UniqueIdentifier(data.UniqueIdentifier);
                    viewModel.EventName(data.EventName);
                    viewModel.EventType(data.EventType);
                    viewModel.Average(data.Average);

                    viewModel.ActiveEntries(ko.utils.arrayMap(data.ActiveEntries, function(d) {
                        return createObservableEntry(d);
                    }));
                });
        };
        var viewModel = {
            UniqueIdentifier: ko.observable(""),
            EventType: ko.observable(""),
            EventName: ko.observable(""),
            Average: ko.observable(""),
            ActiveEntries: ko.observableArray([]),

            inputName: ko.observable(""),
            inputEmail: ko.observable(""),
            inputAmount: ko.observable(""),

            sendInput: function() {
                var model = {
                    Name: viewModel.inputName(),
                    Email: viewModel.inputEmail(),
                    Amount: parseInt(viewModel.inputAmount()),
                    UniqueIdentifier: viewModel.UniqueIdentifier()
                };
                var settings = {
                    url: "/api/weddyApi/FillIn/",
                    data: model,
                    type: "POST",
                    success: function() {
                        getDetails(viewModel.UniqueIdentifier());
                    }
                };
                $.ajax(settings);
            },
        };
        return {
            viewModel: viewModel,
            getDetails: getDetails
        };
    };
})();