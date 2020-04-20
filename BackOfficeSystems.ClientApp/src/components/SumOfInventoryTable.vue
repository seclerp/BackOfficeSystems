<template>
    <div>
        <md-table v-model="data" md-sort="name" md-sort-order="asc" md-card>
            <md-table-toolbar>
                <h1 class="md-title">Sum of inventory</h1>
            </md-table-toolbar>

            <md-table-row slot="md-table-row" slot-scope="{ item }">
                <md-table-cell md-label="ID" md-numeric md-sort-by="brandId">{{ item.brandId }}</md-table-cell>
                <md-table-cell md-label="Name" md-sort-by="name">{{ item.name }}</md-table-cell>
                <md-table-cell md-label="Count" md-numeric md-sort-by="count">{{ item.count }}</md-table-cell>
            </md-table-row>
        </md-table>
    </div>
</template>

<script>
    export default {
        name: "SumOfInventoryTable",
        props: ['data']
    }
</script>

<style scoped>
</style>