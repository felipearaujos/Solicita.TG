function CreateComboBox(cmbSelector, txtSelector, result) {
    var ddl = $(cmbSelector.toString());

    ddl.empty();
    $(document.createElement('option'))
            .attr('value', "")
            .text('Selecione...')
            .appendTo(ddl);

    $(result).each(function () {
        $(document.createElement('option'))
            .attr('value', this.Id)
            .text(this.value)
            .appendTo(ddl);
    });

    $(cmbSelector.toString() + ' option').each(function () {
        if ($(this).text() == $(txtSelector.toString()).val()) {
            $(ddl).val($(this).val());
            $(txtSelector.toString()).val($(this).val());
            return;
        }
    });
}

function RedirectTo(page) {
    window.location = page;
}


function MensagemDeErro(xhr) {
    var msg = JSON.parse(xhr.responseText);
    $("#msgErro").text(msg.Message);
}

