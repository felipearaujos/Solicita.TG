

$("#chkResponsavelEspecifico").click(function (e) {
    debugger;
    if ($("#chkResponsavelEspecifico").prop('checked'))
        $("#divTxtResponsavel").show();
    else
        $("#divTxtResponsavel").hide();
});


$("#btnTransferirResponsabilidade").click(function (e) {
    debugger;
    if ($("#txtResponsavelEspecificoTransf").val() == undefined || $("#txtResponsavelEspecificoTransf").val() == '') {

        $("#txtResponsavelEspecificoTransf").val('00000000-0000-0000-0000-000000000000');
    }

    $.ajax(
        {
            type: 'POST',
            url: '/Requerimento/TransferirResponsabilidade',
            data: {
                requerimento: $("#hdnId").val(), observacao: $("#txtObservacaoTransf").val(),
                idResponsavel: $("#txtResponsavelEspecificoTransf").val(), tipoResponsavel: parseInt($("#txtTipoResponsavelTransf").val())
            },
            success: function (data) {
                if (data.Success == true) {
                    $.ajax(
                        {
                            type: 'POST',
                            url: '/Requerimento/EditSolicitacaoDeDocumentos',
                            data: { id: $("#hdnId").val() },
                            success: function (data) {
                                debugger;
                                $('.btnFechar').click();
                                $('#contentPage').html(data);
                            },
                            error: function (data) {
                                alert(data.Message);
                            }
                        });
                }
                else {
                    alert(data.Message);
                }
            },
            error: function (data) {
                alert(data.Message);
            }
        });

});

$("#btnConcluir").click(function (e) {
    $.ajax(
        {
            type: 'POST',
            url: '/Requerimento/Concluir',
            data: {
                idRequerimento: $("#hdnId").val(), idResponsavel: $('#txtUsuarioHidden').text(), tipoResponsavel: parseInt($('#txtTipoHidden').text())
            },
            success: function (data) {
                if (data.Success == true) {
                    $.ajax(
                        {
                            type: 'POST',
                            url: '/Requerimento/EditSolicitacaoDeDocumentos',
                            data: { id: $("#hdnId").val() },
                            success: function (data) {
                                debugger;
                                $('.btnFechar').click();
                                $('#contentPage').html(data);
                            },
                            error: function (data) {
                                alert(data.Message);
                            }
                        });
                }
                else {
                    alert(data.Message);
                }
            },
            error: function (data) {
                alert(data.Message);
            }
        });

});



$("#btnCancelar").click(function (e) {
    debugger;
    $.ajax(
        {
            type: 'POST',
            url: '/Requerimento/Cancelar',
            data: {
                idRequerimento: $("#hdnId").val(), motivo: $("#txtMotivoCancelamento").val(), idResponsavel: $('#txtUsuarioHidden').text(), tipoResponsavel: parseInt($('#txtTipoHidden').text())
            },
            success: function (data) {
                if (data.Success == true) {
                    $.ajax(
                        {
                            type: 'POST',
                            url: '/Requerimento/EditSolicitacaoDeDocumentos',
                            data: { id: $("#hdnId").val() },
                            success: function (data) {
                                debugger;
                                $('.btnFechar').click();
                                $('#contentPage').html(data);
                            },
                            error: function (data) {
                                alert(data.Message);
                            }
                        });
                }
                else {
                    alert(data.Message);
                }
            },
            error: function (data) {
                alert(data.Message);
            }
        });

});


$("#btnIndeferir").click(function (e) {
    $.ajax(
        {
            type: 'POST',
            url: '/Requerimento/Indeferir',
            data: {
                idRequerimento: $("#hdnId").val(), motivo: $("#txtMotivoIndeferir").val(), idResponsavel: $('#txtUsuarioHidden').text(), tipoResponsavel: parseInt($('#txtTipoHidden').text())
            },
            success: function (data) {
                if (data.Success == true) {
                    $.ajax(
                        {
                            type: 'POST',
                            url: '/Requerimento/EditSolicitacaoDeDocumentos',
                            data: { id: $("#hdnId").val() },
                            success: function (data) {
                                debugger;
                                $('.btnFechar').click();
                                $('#contentPage').html(data);
                            },
                            error: function (data) {
                                debugger;
                                alert(data.Message);
                            }
                        });
                }
                else {
                    alert(data.Message);
                }
            },
            error: function (data) {
                alert(data.Message);
            }
        });

});