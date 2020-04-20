<template>
    <div class="md-layout md-gutter md-alignment-top-center">
        <div class="md-layout-item md-alignment-top-center md-size-25 md-medium-size-33 md-small-size-50 md-xsmall-size-100">
            <div>
                <form novalidate class="md-layout" @submit.prevent="validateBrand">
                    <md-card class="md-layout-item">
                        <md-card-header>
                            <div class="md-title">Create brand</div>
                        </md-card-header>

                        <md-card-content>
                            <md-field :class="getValidationClass('name')">
                                <label for="brandName">Name</label>
                                <md-input type="text" name="brandName" id="brandName" v-model="form.name" :disabled="sendingRequest" />
                                <span class="md-error" v-if="!$v.form.name.required">Brand name is required</span>
                                <span class="md-error" v-else-if="!$v.form.name.minLength">Brand name minimal length is 1</span>
                                <span class="md-error" v-else-if="!$v.form.name.maxLength">Brand name maximum length is 100</span>
                            </md-field>
                        </md-card-content>

                        <md-progress-bar md-mode="indeterminate" v-if="sendingRequest" />

                        <md-card-actions>
                            <md-button type="submit" class="md-primary" :disabled="sendingRequest">Create</md-button>
                        </md-card-actions>
                    </md-card>

                    <md-snackbar :md-active.sync="brandCreated">Brand was created successfully</md-snackbar>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
    import { validationMixin } from 'vuelidate'
    import {
        required,
        minLength,
        maxLength
    } from 'vuelidate/lib/validators'

    export default {
        name: "CreateBrand",
        props: ['data'],
        mixins: [validationMixin],
        data: () => ({
            form: {
                name: null,
            },
            brandCreated: false,
            sendingRequest: false
        }),
        validations: {
            form: {
                name: {
                    required,
                    minLength: minLength(1),
                    maxLength: maxLength(100)
                }
            }
        },
        methods: {
            getValidationClass(fieldName) {
                const field = this.$v.form[fieldName]

                if (field) {
                    return {
                        'md-invalid': field.$invalid && field.$dirty
                    }
                }
            },
            clearForm () {
                this.$v.$reset()
                this.form.name = null
            },
            async validateBrand () {
                this.$v.$touch()

                if (!this.$v.$invalid) {
                    await this.createBrand()
                }
            },
            async createBrand () {
                this.sendingRequest = true;
                let response = await fetch(
                    `${process.env.VUE_APP_API_ROOT}/brand`,
                    {
                        method: 'POST',
                        body: JSON.stringify({
                            name: this.form.name
                        }),
                        headers: {
                            'Content-Type': 'application/json',
                            'Accept': 'application/json'
                        }
                    }
                );
                this.sendingRequest = false;
                if (response.ok) {
                    this.clearForm();
                    this.brandCreated = true;
                }
            }
        }
    }
</script>

<style scoped>
    .md-layout {
        width: 100%;
    }
</style>