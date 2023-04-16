

var totalField = 1;
$(document).ready(function () {

    $('#contentBody_ddlProductType').change(function (e) {

        changeVariant();
    });


    function changeVariant() {
        if ($('#contentBody_ddlProductType').val() == '1') {
            $('#contentBody_divVarient').removeClass('disNone');
            addNewVarient(totalField);

        } else {
            $('#contentBody_divVarient').addClass('disNone');

            $('#varientArea').html("");
        }
    }


    // Add new varient

    $('#btnAddNewVarient').click(function () {
        addNewVarient(totalField);

    });



    var iVariantCounter = 0;
    var fieldAddedCounter = 0;
    function addNewVarient(totalField) {
        if (fieldAddedCounter >= totalField) {
            alert("You can not add more then total field");
            return;
        }

        $('#varientArea').append('<div class="variant-single form-group">'
                                        + '<div class="col-xs-5 col-sm-5 col-md-5 field">'
                                            + '<select class="ddlField" id="ddlField_' + iVariantCounter + '" class="form-control"></select>'
                                         + '</div>'
                                        + '<div class="col-xs-5 col-sm-5 col-md-5 attribute">'
                                            + '<select class="ddlAttr" id="ddlAttr_' + iVariantCounter + '" class="form-control"></select>'
                                        + '</div>'
                                        + '<div class="col-xs-2 col-sm-2 col-md-2">'
                                            + '<input type="button" class="closeVariant btn btn-danger" value="X" />'
                                        + '</div>'
                                    + '</div>');

        closeEvent();
        changeFieldValue();
        var ddlField = $('#varientArea .field select').last().attr('id');
        fieldVariantSearch(ddlField);

        iVariantCounter++;
        fieldAddedCounter++;

        changeAttribute();

    }



    // close varient field
    var deleteItem = 0;
    function closeEvent() {
        var i = 0;
        $('.closeVariant').on('click', function () {
            $(this).closest("div.variant-single").hide();
            if (i == 0)
                fieldAddedCounter--;
            i++;
        });
    }


    // Change field value
    function changeFieldValue() {
        $('.ddlField').change(function () {
            var fieldValue = $(this).val();
            var attrId = $(this).attr('id');
            var getDigit = attrId.split('_')[1];
            var ddlAttribute = 'ddlAttr_' + getDigit;
            attributeVariantSearch(fieldValue, ddlAttribute);

        });

        var oldField = "0";
        $('.ddlField').on('select2:close', function (e) {
            var fieldValue = $(this).val();

            AddOrRemoveFieldRecord(oldField, fieldValue);
        });


        $('.ddlField').on('select2:open', function (e) {

            oldField = $(this).val();
        });

    }

    function changeAttribute() {

        $('.ddlAttr').change(function () {

            var fieldId_closest = $(this).closest('select').attr('id');
            var get_Id = fieldId_closest.split('_')[1];
            var fieldValue = $('#ddlField_' + get_Id + '').val();
            var attrValue = $('#ddlAttr_' + get_Id + '').val();

            attrValue = attrValue.toString().replace('NaN', ' ');

            CreateJSON(fieldValue, attrValue);
        });

    }




    // CTEATE JSON 
    var variantData = [];
    function CreateJSON(fieldValue, attrValue) {

        // iterate over each element in the array
        var isUpdate = false;
        for (var i = 0; i < variantData.length; i++) {
            // look for the entry with a matching `code` value
            if (variantData[i].field == fieldValue) {
                variantData[i].attr = attrValue;
                isUpdate = true;
            }
        }

        if (!isUpdate) {
            var data = { "field": fieldValue, "attr": attrValue };
            variantData.push(data);
        }


        $('#contentBody_hiddenAttributeJosnValue').val(JSON.stringify(variantData));
    }




    

});




var fieldRecordStrage;
function AddOrRemoveFieldRecord(filedRecord, newFieldRecord) {
    var currentFieldRecord = $('#contentBody_lblSelectedFieldRecord').text();
    console.log("currentFieldRecord:", currentFieldRecord);
    var totalFieldRecord = currentFieldRecord;
    if (currentFieldRecord != "") {
        var splitFieldRecord = currentFieldRecord.split(',');
        for (var i = 0; i < splitFieldRecord.length; i++) {

            if (filedRecord == splitFieldRecord[i]) {
                totalFieldRecord = currentFieldRecord.replace(filedRecord, newFieldRecord);
            }
            else {
                totalFieldRecord += "," + filedRecord;
            }
        }
    }

    $('#contentBody_lblSelectedFieldRecord').text(totalFieldRecord);


}




