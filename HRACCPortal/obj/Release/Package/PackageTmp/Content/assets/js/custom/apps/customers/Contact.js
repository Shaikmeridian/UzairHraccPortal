﻿"use strict";


// Class definition
var KTModalContactsAdd = function () {
    var submitButton;
    var cancelButton;
    var closeButton;
    var validator;
    var form;
    var modal;

    // Init form inputs
    var handleForm = function () {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'ContactName': {
                        validators: {
                            stringLength: {
                                max: 60,
                                message: 'The name must be less than 60 characters',
                            },
                            notEmpty: {
                                message: 'Name is required'
                            }
                        }
                    },

                    'ContactPhone': {
                        validators: {
                            notEmpty: {
                                message: 'Phone is required'
                            }
                        }
                    },
                    'ContactEmail': {
                        validators: {
                            emailAddress: {
                                message: 'The value is not a valid email address'
                            },
                            notEmpty: {
                                message: 'Email is required'
                            }
                        }
                    },
                    'ContactAddress1': {
                        validators: {

                            stringLength: {
                                max: 100,
                                message: 'Address cannot cannot be more than 100 characters',
                            },
                            notEmpty: {
                                message: 'Address is required'
                            }
                        }
                    },
                    'ContactCity': {
                        validators: {
                            regexp: {
                                regexp: /^[a-zA-z] ?([a-zA-z]|[a-zA-z] ).*[a-zA-z]$/,
                                message: 'Please enter valid city with min 3 characters',
                            },
                            stringLength: {
                                max: 50,
                                message: 'City cannot cannot be more than 50 characters',
                            },
                            notEmpty: {
                                message: 'City is required'
                            }
                        }
                    },
                    'ContactState': {
                        validators: {
                            notEmpty: {
                                message: 'State is required'
                            }
                        }
                    },
                    'ContactZip': {
                        validators: {
                            regexp: {
                                regexp: /^\d{5}$/,
                                message: 'The US zip code must contain 5 digits',
                            },
                            notEmpty: {
                                message: 'ZipCode is required'
                            }
                        }
                    },
                    'ContactType': {
                        validators: {
                            notEmpty: {
                                message: 'ContactType is required'
                            }
                        }
                    },
                    
                    'ContactFEID': {
                        validators: {
                            stringLength: {
                                max: 20,
                                message: 'FEID cannot be more than 20 characters & must be alphanumeric',
                            },
                            notEmpty: {
                                message: 'FEID is required'
                            }
                        }
                    }

                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        // Revalidate country field. For more info, plase visit the official plugin site: https://select2.org/
        //$(form.querySelector('[name="country"]')).on('change', function() {
        //    // Revalidate the field when an option is chosen
        //    validator.revalidateField('country');
        //});

        // Action buttons
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();
            //debugger;
            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {


                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable submit button whilst loading
                        //submitButton.disabled = true;
                        var contact = {};

                        contact.ContactName = form.ContactName.value;
                        contact.ContactPhone = form.ContactPhone.value;
                        contact.ContactEmail = form.ContactEmail.value;
                        contact.ContactAddress1 = form.ContactAddress1.value;
                        contact.ContactAddress2 = form.ContactAddress2.value;
                        contact.ContactCity = form.ContactCity.value;
                        contact.ContactState = form.ContactState.value;
                        contact.ContactType = form.ContactType.value;
                        contact.ContactZip = form.ContactZip.value;
                        contact.ContactFEID = form.ContactFEID.value;
                        var actv = $('input#isActive').prop('checked');
                        contact.isActive = actv;

                        contact.ContactIdPK = form.ContactIdPK.value;
                        console.log("contact:" + JSON.stringify(contact))
                        $.ajax({
                            type: "POST",
                            url: '/Contact/AddContact',
                            data: '{contact: ' + JSON.stringify(contact) + '}',
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                submitButton.removeAttribute('data-kt-indicator');

                                // alert("Data has been added successfully.");
                                if (response.message == "success" || response.message == "updated") {

                                    Swal.fire({
                                        text: "Form has been successfully submitted!",
                                        icon: "success",
                                        buttonsStyling: false,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then(function (result) {
                                        if (result.isConfirmed) {
                                            // Hide modal
                                            modal.hide();

                                            // Enable submit button after loading
                                            submitButton.disabled = false;

                                            // Redirect to customers list page
                                            window.location = form.getAttribute("data-kt-redirect");
                                        }
                                    });
                                } else {
                                    console.log(response.message);
                                    Swal.fire({
                                        text: response.message,
                                        icon: "error",
                                        buttonsStyling: false,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    });
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                submitButton.removeAttribute('data-kt-indicator');

                                console.log("Custom error : " + jqXHR.responseText + " Status: " + textStatus + " Http error:" + errorThrown);

                                Swal.fire({
                                    text: "Custom error : " + jqXHR.responseText + " Status: " + textStatus + " Http error:" + errorThrown,
                                    icon: "error",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                });

                            }
                        });

                    } else {

                        Swal.fire({
                            text: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                });
            }
        });

        cancelButton.addEventListener('click', function (e) {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-success"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form	
                    modal.hide(); // Hide modal				
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        });

        closeButton.addEventListener('click', function (e) {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-success"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form	
                    modal.hide(); // Hide modal				
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        })
    }

    return {
        // Public functions
        init: function () {
            // Elements
            modal = new bootstrap.Modal(document.querySelector('#kt_modal_add_contact'));

            form = document.querySelector('#kt_modal_add_contact_form');
            submitButton = form.querySelector('#kt_modal_add_contact_submit');
            cancelButton = form.querySelector('#kt_modal_add_contact_cancel');
            closeButton = form.querySelector('#kt_modal_add_contact_close');

            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTModalContactsAdd.init();
});