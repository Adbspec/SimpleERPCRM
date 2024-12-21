var colors = ["#727cf5", "#0acf97", "#fa5c7c", "#ffbc00"],
  dataColors = $("#campaign-sent-chart").data("colors");
dataColors && (colors = dataColors.split(","));
var options1 = {
  chart: { type: "bar", height: 60, sparkline: { enabled: !0 } },
  plotOptions: { bar: { columnWidth: "60%" } },
  colors: colors,
  series: [{ data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54] }],
  labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
  xaxis: { crosshairs: { width: 1 } },
  tooltip: {
    fixed: { enabled: !1 },
    x: { show: !1 },
    y: {
      title: {
        formatter: function (o) {
          return "";
        },
      },
    },
    marker: { show: !1 },
  },
};
new ApexCharts(
  document.querySelector("#campaign-sent-chart"),
  options1
).render();
colors = ["#727cf5", "#0acf97", "#fa5c7c", "#ffbc00"];
(dataColors = $("#new-leads-chart").data("colors")) &&
  (colors = dataColors.split(","));
var options2 = {
  chart: { type: "line", height: 60, sparkline: { enabled: !0 } },
  series: [{ data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54] }],
  stroke: { width: 2, curve: "smooth" },
  markers: { size: 0 },
  colors: colors,
  tooltip: {
    fixed: { enabled: !1 },
    x: { show: !1 },
    y: {
      title: {
        formatter: function (o) {
          return "";
        },
      },
    },
    marker: { show: !1 },
  },
};
