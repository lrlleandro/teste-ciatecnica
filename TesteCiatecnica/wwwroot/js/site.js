$(document).ready(function () {
    $('.date').mask('00/00/0000')
    $('.time').mask('00:00:00')
    $('.date_time').mask('00/00/0000 00:00:00')
    $('.cep').mask('00000-000', { reverse: true })
    $('.phone').mask('0000-0000')
    $('.phone_with_ddd').mask('(00) 0000-0000')
    $('.phone_us').mask('(000) 000-0000')
    $('.mixed').mask('AAA 000-S0S')
    $('.cpf').mask('000.000.000-00', { reverse: true })
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true })
    $('.money').mask('000.000.000.000.000,00', { reverse: true })
    $('.money2').mask("#.##0,00", { reverse: true })
    $('.ip_address').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
        translation: {
            'Z': {
                pattern: /[0-9]/, optional: true
            }
        }
    })
    $('.ip_address').mask('099.099.099.099')
    $('.percent').mask('##0,00%', { reverse: true })
    $('.clear-if-not-match').mask("00/00/0000", { clearIfNotMatch: true })
    $('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" })
    $('.fallback').mask("00r00r0000", {
        translation: {
            'r': {
                pattern: /[\/]/,
                fallback: '/'
            },
            placeholder: "__/__/____"
        }
    })
    $('.selectonfocus').mask("00/00/0000", { selectOnFocus: true })

    $('[data-toggle="tooltip"]').tooltip()
})

$("button").click(e => handleButtons(e))

$('#slcCustomerType').change(() => {
    prepareCustomerModal($('#slcCustomerType').val())
})

$('#ZipCode').blur(() => {
    searchAddressByZipCode($('#ZipCode').val())
})

$('#ZipCode').keypress((e) => {
    if (e.key === "Enter") {
        searchAddressByZipCode($('#ZipCode').val())
    }
})

$('#btnSaveCustomer').click(() => {
    let url = ''
        
    const data = normalizeCustomerObject($('#frmCustomer').serializeObject())

    if (data.customerId == 0) {
        url = '/customers/create'
    } else {
        url = '/customers/edit'
    }

    $.post(url, data)
        .done((data) => {
            if (data.hasValidationErrors) {
                const arrValidationErrors = data.validationErrors.trim().split('|')

                for (const validationError of arrValidationErrors) {
                    if (validationError != '') {
                        $.toast({
                            text: validationError,
                            showHideTransition: 'slide',
                            icon: 'warning',
                            hideAfter: 5000
                        })
                    }
                }
            } else {
                $.toast({
                    text: data.toString(),
                    showHideTransition: 'slide',
                    icon: 'info',
                    hideAfter: 5000
                })
                refreshCustomerList()
                $('#modalCustomer').fadeOut()
            }
        })
        .fail((error) => {            
            $.toast({
                text: error.responseText.toString(),
                showHideTransition: 'slide',
                icon: 'error',
                hideAfter: 5000
            })
        })
})

const clearModal = () => {
    $('.modal').find('input').val('')
}

const prepareCustomerModal = (customerType) => {
    if (customerType == "PhysicalPerson" || customerType == 0) {
        $('label[for="SSNorEIN"').html("CPF")
        $('#SSNorEIN').removeClass("cnpj")
        $('#SSNorEIN').mask('000.000.000-00')
        $('#SSNorEIN').addClass("cpf")
        $('label[for="NameOrCompanyName"').html("Nome")
        $('label[for="BirthDate"').html("Data de Nascimento")
        $('#birthdate-container').fadeIn()
        $('label[for="LastNameOrTradingName"').html("Sobrenome")
        $('#LastNameOrTradingName').attr('maxlength', 15)
    } else {
        $('label[for="SSNorEIN"').html("CNPJ")
        $('#SSNorEIN').removeClass("cpf")
        $('#SSNorEIN').mask('00.000.000/0000-00')
        $('#SSNorEIN').addClass("cnpj")
        $('label[for="NameOrCompanyName"').html("Razão Social")
        $('#birthdate-container').fadeOut()
        $('label[for="LastNameOrTradingName"').html("Nome Fantasia")
    }
}

const prepareCustomerDetailsModal = (customer) => {
    const container = $('.details-customer-content')
    const customerType = customer.customerType

    container.html('')
    let name, value

    for (const property in customer) {
        switch (property.toLowerCase()) {
            case "ssnorein":
                if (customerType == 0) {
                    name = "CPF"
                    value = customer[property]
                    container.append(`<input id="cpf" class="cpf" value="${value}" />`)
                    let cpfEl = container.find('#cpf')
                    cpfEl.mask('000.000.000-00')
                    container.append(`<dt>${name}</dt><dd>${cpfEl.val()}</dd>`)
                    cpfEl.remove()
                } else {
                    name = "CNPJ"
                    value = customer[property]
                    container.append(`<input id="cnpj" class="cnpj" value="${value}" />`)
                    let cnpjEl = container.find('#cnpj')
                    cnpjEl.mask('00.000.000/0000-00')
                    container.append(`<dt>${name}</dt><dd>${cnpjEl.val()}</dd>`)
                    cnpjEl.remove()
                }
                break
            case "nameorcompanyname":
                if (customerType === 0) {
                    name = "Nome"
                } else {
                    name = "Razão Social"
                }
                value = customer[property]
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
            case "lastnameortradingname":
                if (customerType === 0) {
                    name = "Sobrenome"
                } else {
                    name = "Nome Fantasia"
                }
                value = customer[property]
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
            case "birthdate":
                if (customerType === 0) {
                    name = "Data de Nascimento"
                    value = new Date(customer[property]).toLocaleDateString()
                    container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                }
                break
            case "zipcode":
                name = "CEP"
                value = customer[property]
                container.append(`<input id="cep" class="cep" value="${value}" />`)
                let cepEl = container.find('#cep')
                cepEl.mask('00000-000')
                container.append(`<dt>${name}</dt><dd>${cepEl.val()}</dd>`)
                cepEl.remove()
                break
            case "street":
                name = "Logradouro"
                value = customer[property]
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
            case "number":
                name = "Número"
                value = customer[property]
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
            case "complement":
                name = "Complemento"
                value = (customer[property]) ? customer[property] : ''
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
            case "neighborhood":
                name = "Bairro"
                value = customer[property]
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
            case "city":
                name = "Cidade"
                value = customer[property]
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
            case "state":
                name = "UF"
                value = customer[property]
                container.append(`<dt>${name}</dt><dd>${value}</dd>`)
                break
        }
    }
}

const searchAddressByZipCode = (zipCode) => {
    cep(zipCode)
        .then((data) => {
            $('#State').val(data.state)
            $('#City').val(data.city)
            $('#Neighborhood').val(data.neighborhood)
            $('#Street').val(data.street)
        }, (error) => {
            $('#Street').val('')
            $('#City').val('')
            $('#Number').val('')
            $('#Neighborhood').val('')
            $('#Complement').val('')
            $('#State').val('')
            $('#ZipCode').val('')

            $.toast({
                text: `CEP não encontrado`,
                showHideTransition: 'slide',
                icon: 'warning',
                hideAfter: 5000
            })
        })
}

const normalizeCustomerObject = (object) => {
    object.SSNorEIN = object.SSNorEIN.replace(/[^0-9]/g, "")
    object.ZipCode = object.ZipCode.replace(/[^0-9]/g, "")
    const arrBirthDate = object.BirthDate.split('/')
    object.BirthDate = `${arrBirthDate[2]}-${arrBirthDate[1]}-${arrBirthDate[0]}`
    return object
}

const refreshCustomerList = () => {
    const url = `/customers/list`

    $.get(url)
        .done((data) => {
            $('#customers-list-content').html('')

            for (const customer of data) {
                $('#customers-list-content').append(`
                    <tr>
                        <td>${customer.ssNorEIN}</td>
                        <td>${customer.nameOrCompanyName}</td >
                        <td>${customer.lastNameOrTradingName}</td>
                        <td>
                            <button data-action="editCustomer" data-id="${customer.customerId}" class="btn" data-toggle="tooltip" data-placement="top" title="Editar"><i class="fas fa-pencil-alt"></i></i></button>
                            <button data-action="detailCustomer" data-id="${customer.customerId}" class="btn" data-toggle="tooltip" data-placement="top" title="Detalhes"><i class="fa fa-list"></i></button>
                            <button data-action="deleteCustomer" data-id="${customer.customerId}" class="btn" data-toggle="tooltip" data-placement="top" title="Excluir"><i class="fa fa-trash"></i></button>
                        </td>
                    </tr>
                `)
                
                $("button").click(e => handleButtons(e))
            }
        })
        .fail((error) => {
            $.toast({
                text: error.responseText.toString(),
                showHideTransition: 'slide',
                icon: 'error',
                hideAfter: 5000
            })
        })            
}

const handleButtons = (e) => {
    const element = $(e.currentTarget)
    const action = element.data("action")

    switch (action) {
        case "addCustomer":
            clearModal()
            prepareCustomerModal($('#slcCustomerType').val())
            $('#modalCustomer').fadeIn()
            break
        case "editCustomer":
            const customerIdForEdit = element.data("id")
            const urlEdit = `/customers/read/${customerIdForEdit}`

            $.get(urlEdit)
                .done((data) => {
                    clearModal()
                    loadModalFromObject(data)
                    prepareCustomerModal($('#slcCustomerType').val())
                    $('#modalCustomer').fadeIn()
                })
                .fail((error) => {                    
                    $.toast({
                        text: error.responseText.toString(),
                        showHideTransition: 'slide',
                        icon: 'error',
                        hideAfter: 5000
                    })
                })
            break
        case "detailCustomer":
            const customerIdForDetails = element.data("id")
            const urlDetails = `/customers/read/${customerIdForDetails}`

            $.get(urlDetails)
                .done((data) => {
                    prepareCustomerDetailsModal(data)                    
                    $('#modalDetailsCustomer').fadeIn()
                })
                .fail((error) => {                    
                    $.toast({
                        text: error.responseText.toString(),
                        showHideTransition: 'slide',
                        icon: 'error',
                        hideAfter: 5000
                    })
                })
            break
        case "deleteCustomer":
            let customerIdForDelete = element.data("id")
            $('#modalDeleteCustomer').fadeIn()

            $('#btnConfirmDeleteCustomer').click(() => {
                const url = `/customers/delete/${customerIdForDelete}`
                customerIdForDelete = ''

                $.post(url)
                    .done((data) => {
                        $.toast({
                            text: data.toString(),
                            showHideTransition: 'slide',
                            icon: 'info',
                            hideAfter: 5000
                        })
                        refreshCustomerList()
                        $('#modalDeleteCustomer').fadeOut()
                    }).
                    fail((error) => {
                        $.toast({
                            text: error.responseText.toString(),
                            showHideTransition: 'slide',
                            icon: 'error',
                            hideAfter: 5000
                        })
                        
                        $('#modalDeleteCustomer').fadeOut()
                    })
            })
            break
    }
}

const loadModalFromObject = (customer) => {
    $('#slcCustomerType').val((customer.customerType == 0) ? "PhysicalPerson" : "LegalPerson")
    $('#CustomerId').val(customer.customerId)
    $('#CustomerType').val(customer.customerType)
    $('#SSNorEIN').val(customer.ssNorEIN)
    $('#NameOrCompanyName').val(customer.nameOrCompanyName)
    $('#LastNameOrTradingName').val(customer.lastNameOrTradingName)
    $('#BirthDate').val(new Date(customer.birthDate).toLocaleDateString())
    $('#AddressId').val(customer.addressId)
    $('#ZipCode').val(customer.zipCode)
    $('#ZipCode').val(customer.zipCode)
    $('#Street').val(customer.street)
    $('#Number').val(customer.number)
    $('#Complement').val((customer.complement) ? customer.complement : '')
    $('#Neighborhood').val(customer.neighborhood)
    $('#City').val(customer.city)
    $('#State').val(customer.state)
}