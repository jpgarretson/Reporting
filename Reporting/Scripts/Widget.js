jQuery(function ($) {

    function drawPieChart(placeholder, data, position) {
        $.plot(placeholder, data, {
            series: {
                pie: {
                    show: true,
                    tilt: 0.8,
                    highlight: {
                        opacity: 0.25
                    },
                    stroke: {
                        color: '#fff',
                        width: 2
                    },
                    startAngle: 2
                }
            },
            legend: {
                show: true,
                position: position || "ne",
                labelBoxBorderColor: null,
                margin: [-30, 15]
            }
            ,
            grid: {
                hoverable: true,
                clickable: true
            }
        })
    }

    function drawLineChart(placeholder, data) {
        $.plot(placeholder, data, {
            hoverable: true,
            shadowSize: 0,
            series: {
                lines: { show: true },
                points: { show: true }
            },
            xaxis: {
                tickLength: 0
            },
            yaxis: {
                ticks: 10,
                min: -2,
                max: 2,
                tickDecimals: 3
            },
            grid: {
                backgroundColor: { colors: ["#fff", "#fff"] },
                borderWidth: 1,
                borderColor: '#555'
            }
        });
    }

    // consts
    var minCord = {x: -60, y: -57};
    var maxCord = {x: 60, y: -60};
    var radius = 90;

    // some calculations
    var startAngle = (6.2831 + Math.atan2(minCord.y, minCord.x));
    var endAngle = Math.atan2(maxCord.y, maxCord.x);
    var degreesSweep = (-endAngle) + startAngle;




    $('.widget-box').each(function (index, value) {

        var widgetbox = $(this);
        var data = JSON.parse((widgetbox).attr("data-model"));
        var placeholder = widgetbox.find('[id^="piechart-placeholder-"]').css({ 'width': '90%', 'min-height': '150px' });
        var id = $(placeholder).attr("data-id");
        var dataUrl = $(placeholder).attr("data-url");

        function WidgetViewModel(data) {

            var self = this;

            self.QueryOptions = data.QueryOptions;

            self.Id = ko.observable(data.Id);
            self.Query = ko.observable(data.Query);

            self.Title = ko.computed(function () {
                return data.Title + " #" + self.Id();
            }, this);

            self.dataUrl = ko.computed(function () {
                return data.DataUrl;
            }, this);

            self.updateQuery = function (query) {
                self.Query(query);
                self.drawWidget();
            };

            self.Type = ko.observable(data.Type);

            self.obj = ko.computed(function () {

                var obj = new Object();

                obj.Id = self.Id();
                obj.Title = self.Title();
                obj.DataUrl = self.dataUrl();
                obj.Query = self.Query();
                obj.QueryOptions = self.QueryOptions;
                obj.Type = self.Type();

                return obj;
            });

            //Behavious 
            self.drawWidget = function (obj) {
                $.ajax({
                    url: self.dataUrl(),
                    type: 'post',
                    data: self.obj(),
                    dataType: 'json',
                    async: true,
                    success: function (data) {

                        if (self.Type() == "PieChart") {

                            drawPieChart(placeholder, data);

                            /**
                            we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
                            so that's not needed actually.
                            */

                            placeholder.data('chart', data);
                            placeholder.data('draw', drawPieChart);

                            var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');
                            var previousPoint = null;

                            placeholder.on('plothover', function (event, pos, item) {
                                if (item) {
                                    if (previousPoint != item.seriesIndex) {
                                        previousPoint = item.seriesIndex;
                                        var tip = item.series['label'] + " : " + item.series['percent'] + '%';
                                        $tooltip.show().children(0).text(tip);
                                    }
                                    $tooltip.css({ top: pos.pageY + 10, left: pos.pageX + 10 });
                                } else {
                                    $tooltip.hide();
                                    previousPoint = null;
                                }

                            });
                        }

                        if (self.Type() == "LineChart") {

                            drawLineChart(placeholder, data);

                        }

                    },
                    error: function (data, textStatus, jqXHR) {
                        alert(jqXHR.responseText);
                    }
                });
            };

            self.drawWidget();
        }

        var el = $(widgetbox).parent().get(0);
        ko.applyBindings(new WidgetViewModel(data), el);

    });






});