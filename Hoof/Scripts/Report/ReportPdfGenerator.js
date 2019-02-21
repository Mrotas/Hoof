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

        headers.push({ text: 'Informacja o pozyskaniu zwierzyny w obwodzie łowieckim nr 20', style: 'header', margin: [0, 0, 0, 5]} );
        headers.push({ text: 'w sezonie łow.  ........   za okres  od .......... roku   do .......... roku', style: 'header', margin: [0, 0, 0, 5]} );

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

        $.each(data.MonthlyReportKindGameModels, function(index, value) {
            body.push([
                { text: value.KindName, colSpan: 2, bold: true, style: 'point' },
                { text: '' },
                { text: value.Culls, style: 'cell' },
                { text: value.LossesWithCatches, style: 'cell' },
                { text: value.ExecutionTotal, style: 'cell' },
                { text: value.HuntPlanCulls, style: 'cell' },
                { text: value.ExecutionLeft, style: 'cell' },
                { text: value.Note, style: 'cell' }
            ]);
        });

        return body;
    };

    var getFodderRow = function (data) {
        var fodderRow = [];

        fodderRow.push([
            { text: 'W obwodzie wyłożono następujące ilości karmy :', colSpan: 2, rowSpan: 4, style: 'columnHeader', border: [false, false, false, false] },
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: 'objętościowej suchej', colSpan: 2, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', colSpan: 3, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false] },
            { text: '', style: 'cell', border: [false, false, false, false] },
            { text: 'kg', style: 'cell', border: [false, false, false, false]}
        ]);

        fodderRow.push([
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: 'objętościowej soczystej', colSpan: 2, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', colSpan: 3, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: 'kg', style: 'cell', border: [false, false, false, false]}
        ]);

        fodderRow.push([
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: 'treściwej', colSpan: 2, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', colSpan: 3, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: 'kg', style: 'cell', border: [false, false, false, false]}
        ]);

        fodderRow.push([
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: 'soli', colSpan: 2, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', colSpan: 3, style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: '', style: 'cell', border: [false, false, false, false]},
            { text: 'kg', style: 'cell', border: [false, false, false, false]}
        ]);

        return fodderRow;
    };

    var getReportTable = function (data) {
        var reportTableHeaders = getReportTableHeaders();
        var columnNumberHeaders = getNumbersRow(8);
        var bigGameReportTableBody = getBigGameReportTableBody(data.MonthlyReportBigGameModel);
        var smallGameReportTableBody = getSmallGameReportTableBody(data.MonthlyReportSmallGameModel);
        var fodderRow = getFodderRow(data);

        var reportTable = [];
        reportTable.push(reportTableHeaders);
        reportTable.push(columnNumberHeaders);
        for (let i = 0; i < bigGameReportTableBody.length; i++) {
            reportTable.push(bigGameReportTableBody[i]);
        }
        for (let i = 0; i < smallGameReportTableBody.length; i++) {
            reportTable.push(smallGameReportTableBody[i]);
        }
        for (let i = 0; i < fodderRow.length; i++) {
            reportTable.push(fodderRow[i]);
        }

        return reportTable;
    };

    var getAdministrationInformationRow = function(data) {
        var administrationRow = [];

        administrationRow.push([
            { text:'Informacja o wykonanej poprawie naturalnych warunków bytowania zwierzyny:', style: 'header', pageBreak: 'before', margin: [0, 0, 0, 30]}
        ]);

        $.each(data, function() {
            //TODO: push administration info here
        });

        administrationRow.push({
            text: [
                { text: 'Miejscowość ...........................................................', style: 'paragraph', margin:[0, 30, 0, 0]},
                { text: '(pieczęć, podpis)', alignment: 'right', style: 'cell' }
            ]
        });

        return administrationRow;
    };

    var downloadPdf = function (data) {

        var reportHeaders = getReportHeaders();
        var reportTable = getReportTable(data);
        var administrationInformation = getAdministrationInformationRow(data);

        var docDefinition = {
            pageOrientation: 'portrait',
            pageMargins: [28, 10, 28, 0],
            pageSize: 'A4',
            content: [
                reportHeaders,
                {
                    table: {
                        widths: ['8%', '17%', '11%', '11%', '11%', '13%', '11%', '18%'],
                        body: reportTable,
                        pageBreak: 'before',
                        dontBreakRows: true
                    }
                },
                administrationInformation
            ],
            styles: {
                header: {
                    alignment: 'center',
                    fontSize: 12,
                    bold: true
                },
                columnHeader: {
                    alignment: 'center',
                    fontSize: 9,
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