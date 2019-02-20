var ReportPdfGenerator = function (config) {

    var marketingYearId = config.marketingYearId;
    var reportDateFrom = config.reportFromDate;
    var reportDateTo = config.reportDateTo;

    var getNumbersRow = function (columnsNumber) {
        var numbersRow = [];
        for (var i = 1; i <= columnsNumber; i++) {
            numbersRow.push({ text: i, style: 'numberHeader' });
        }
        return numbersRow;
    }

    var getReportHeaders = function() {
        var headers = [];

        headers.push( { text: 'Informacja o pozyskaniu zwierzyny w obwodzie łowieckim nr 20', style: 'header' } );
        headers.push({ text: 'w sezonie łow.  ........   za okres  od .......... roku   do .......... roku', style: 'header' } );

        return headers;
    }

    var getReportTableHeaders = function() {
        var headers = [];

        headers.push({ text: 'Gatunek zwierzyny', colSpan: 2, style: 'columnHeader' });
        headers.push({ text: '', style: 'columnHeader' });
        headers.push({ text: 'Odstrzelono', style: 'columnHeader' });
        headers.push({ text: 'Sztuki padłe i skłusowane', style: 'columnHeader' });
        headers.push({ text: 'Razem', style: 'columnHeader' });
        headers.push({ text: 'Plan odstrzału w roku gospodarczym', style: 'columnHeader' });
        headers.push({ text: 'Pozostaje do pozyskania', style: 'columnHeader' });
        headers.push({ text: 'Uwagi', style: 'columnHeader' });
        
        return headers;
    };

    var getBigGameReportTableBody = function(data) {
        var body = [];

        var firstColumnRowspan = 0;
        var firstColumnColspan = 0;

        var bigGameKindsWithPortraitColumn = [2, 4, 5, 6];
        
        $.each(data.MonthlyReportKindGameModels, function (index, value) {

            var kindName = value.KindName;
            if (bigGameKindsWithPortraitColumn.includes(value.Kind)) {
                firstColumnRowspan = value.MonthlyReportSubKindGameModels[0].MonthlyReportClassGameModels.length + 4;
            } else {
                firstColumnRowspan = 0;
                firstColumnColspan = 2;
            }

            $.each(value.MonthlyReportSubKindGameModels, function (index, value) {
                
                $.each(value.MonthlyReportClassGameModels, function (index, value) {

                    var cellText = index === 0 ? kindName : '';

                    body.push([
                        { text: cellText, colSpan: firstColumnColspan, rowSpan: firstColumnRowspan, bold: true, style: 'point' },
                        { text: value.ClassName, style: 'cell' },
                        { text: value.Culls, style: 'cell' },
                        { text: value.LossesWithCatches, style: 'cell' },
                        { text: value.ExecutionTotal, style: 'cell' },
                        { text: value.HuntPlanCulls, style: 'cell' },
                        { text: value.ExecutionLeft, style: 'cell' },
                        { text: value.Note, style: 'cell' }
                    ]);
                });

                var cellText = index === 0 ? kindName : '';

                body.push([
                    { text: cellText, colSpan: firstColumnColspan, rowSpan: firstColumnRowspan, bold: true, style: 'point' },
                    { text: value.SubKindName, style: 'cell' },
                    { text: value.Culls, style: 'cell' },
                    { text: value.LossesWithCatches, style: 'cell' },
                    { text: value.ExecutionTotal, style: 'cell' },
                    { text: value.HuntPlanCulls, style: 'cell' },
                    { text: value.ExecutionLeft, style: 'cell' },
                    { text: value.Note, style: 'cell' }
                ]);
            });

            if (bigGameKindsWithPortraitColumn.includes(value.Kind)) {
                body.push([
                    { text: ''},
                    { text: 'ogółem', style: 'cell' },
                    { text: value.Culls, style: 'cell' },
                    { text: value.LossesWithCatches, style: 'cell' },
                    { text: value.ExecutionTotal, style: 'cell' },
                    { text: value.HuntPlanCulls, style: 'cell' },
                    { text: value.ExecutionLeft, style: 'cell' },
                    { text: value.Note, style: 'cell' }
                ]);
            } else {
                body.push([
                    { text: kindName, colSpan: firstColumnColspan, rowSpan: firstColumnRowspan, bold: true, style: 'point' },
                    { text: '' },
                    { text: value.Culls, style: 'cell' },
                    { text: value.LossesWithCatches, style: 'cell' },
                    { text: value.ExecutionTotal, style: 'cell' },
                    { text: value.HuntPlanCulls, style: 'cell' },
                    { text: value.ExecutionLeft, style: 'cell' },
                    { text: value.Note, style: 'cell' }
                ]);
            }
        });

        return body;
    };

    var getSmallGameReportTableBody = function(data) {
        var body = [];



        return body;
    };

    var getReportTable = function (data) {
        var reportTableHeaders = getReportTableHeaders();
        var columnNumberHeaders = getNumbersRow(8);
        var bigGameReportTableBody = getBigGameReportTableBody(data.MonthlyReportBigGameModel);
        //var smallGameReportTableBody = getSmallGameReportTableBody(data.MonthlyReportBigGameModel);

        var reportTable = [];
        reportTable.push(reportTableHeaders);
        reportTable.push(columnNumberHeaders);
        for (let i = 0; i < bigGameReportTableBody.length; i++) {
            reportTable.push(bigGameReportTableBody[i]);
        }
        //for (let i = 0; i < smallGameReportTableBody.length; i++) {
        //    reportTable.push(smallGameReportTableBody[i]);
        //}

        return reportTable;
    };

    var downloadPdf = function (data) {

        var reportHeaders = getReportHeaders();
        var reportTable = getReportTable(data);

        var docDefinition = {
            pageOrientation: 'portrait',
            pageMargins: [28, 28, 28, 28],
            pageSize: 'A4',
            content: [
                reportHeaders,
                {
                    table: {
                        widths: ['8%', '20%', '12%', '12%', '12%', '12%', '12%', '12%'],
                        body: reportTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    }
                }
            ],
            styles: {
                header: {
                    alignment: 'center',
                    fontSize: 12,
                    bold: true
                },
                columnHeader: {
                    alignment: 'center',
                    fontSize: 10,
                    bold: true
                },
                numberHeader: {
                    alignment: 'center',
                    fontSize: 7
                },
                paragraph: {
                    fontSize: 9,
                    bold: true
                },
                point: {
                    fontSize: 9,
                    alignment: 'left'
                },
                cell: {
                    fontSize: 9,
                    alignment: 'center'
                }
            }
        };

        pdfMake.createPdf(docDefinition).download('Meldunek' + reportDateFrom + '-' + reportDateTo + '.pdf');
    }

    $('#generateReportPdf').on('click', function () {
        $.ajax({
            type: "GET",
            url: '/Report/GetMonthlyReportJsonData?dateFrom=' + reportDateFrom + '&dateTo=' + reportDateTo,
            success: function (data) {
                downloadPdf(data);
            },
            error: function () {
                alert('Coś poszło nie tak, proszę odświeżyć stronę.');
            }
        });
    });
}